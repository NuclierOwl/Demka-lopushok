using System;
using System.Collections.Generic;

namespace Lopushok.Hardik.Dao;

public partial class PRODUCTMATERIAL
{
    public string? NAME { get; set; }

    public string? MATIRIAL { get; set; }

    public int? NEOBHODIM_MATERIAL { get; set; }

    public virtual MATERIALS_SHORT_B_IMPORT? MATIRIALNavigation { get; set; }

    public virtual PRODUCT? NAMENavigation { get; set; }
}
