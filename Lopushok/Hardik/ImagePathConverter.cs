using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Lopushok.Converters
{
    public class ImagePathConverter : IValueConverter
    {
        private static Bitmap? _defaultImage;
        private static bool _defaultImageTriedToLoad = false;

        public static ImagePathConverter Instance { get; } = new ImagePathConverter();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                string? imagePath = value as string;

                if (!string.IsNullOrEmpty(imagePath))
                {
                    if (File.Exists(imagePath))
                    {
                        return new Bitmap(imagePath);
                    }

                    string pathInAssets = Path.Combine("Assets", "Imegens", imagePath);
                    if (File.Exists(pathInAssets))
                    {
                        return new Bitmap(pathInAssets);
                    }

                    pathInAssets = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Assets", "Imegens", imagePath);
                    if (File.Exists(pathInAssets))
                    {
                        return new Bitmap(pathInAssets);
                    }
                }

                return LoadDefaultImage();
            }
            catch
            {
                return LoadDefaultImage();
            }
        }

        private object? LoadDefaultImage()
        {
            if (_defaultImage != null)
                return _defaultImage;

            if (!_defaultImageTriedToLoad)
            {
                _defaultImageTriedToLoad = true;

                string[] possiblePaths =
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Imegens", "picture.png"),
                    Path.Combine("Assets", "Imegens", "picture.png")
                };

                foreach (var path in possiblePaths)
                {
                    if (File.Exists(path))
                    {
                        try
                        {
                            _defaultImage = new Bitmap(path);
                            break;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }

            return _defaultImage;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}