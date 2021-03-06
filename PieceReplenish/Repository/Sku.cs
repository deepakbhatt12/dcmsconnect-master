using System.ComponentModel.DataAnnotations;

namespace DcmsMobile.PieceReplenish.Repository
{
    public class Sku
    {
        [Key]
        public int SkuId { get; set; }

        public string Style { get; set; }

        public string Color { get; set; }

        public string Dimension { get; set; }

        public string SkuSize { get; set; }

        public string UpcCode { get; set; }        
    }
}

/*
    $Id$ 
    $Revision$
    $URL$
    $Header$
    $Author$
    $Date$
*/