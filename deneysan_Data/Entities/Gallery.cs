using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deneysan_DAL.Entities
{
    public class Gallery
    {
        public int GalleryId { get; set; }
        public int GalleryGroupId { get; set; }
        public bool Deleted { get; set; }
        public string Image { get; set; }
        public bool TimeCreated { get; set; }
        public bool TimeUpdated { get; set; }
        
    }
}
