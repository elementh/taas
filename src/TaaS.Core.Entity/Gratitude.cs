﻿using System.Collections.Generic;

namespace TaaS.Core.Entity
{
    public class Gratitude
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }
        public List<GratitudeCategory> Categories { get; set; }
    }
}