using System;
using System.Collections.Generic;

namespace Lopushok.Hardik.Dao;

public partial class MATERIALS_SHORT_B_IMPORT
{
    public string Material_Name { get; set; } = null!;

    public string? Type { get; set; }

    public string? Kolichestvo { get; set; }

    public string? Izmerenie { get; set; }

    public string? Kolikhestvo_sklad { get; set; }

    public string? Min { get; set; }

    public string? Cost { get; set; }
}
