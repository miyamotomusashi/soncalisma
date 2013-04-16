﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deneysan_DAL.Entities
{
    public class GalleryGroup
    {
        public int GalleryGroupId { get; set; }
        public string GroupName { get; set; }
        public string Language { get; set; }
        public bool Online { get; set; }
        public bool Deleted { get; set; }
        public bool TimeCreated { get; set; }
        public bool TimeUpdated { get; set; }
    }
}
