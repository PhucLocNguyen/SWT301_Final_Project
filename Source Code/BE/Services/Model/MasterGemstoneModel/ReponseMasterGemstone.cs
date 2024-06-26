﻿using API.Model.DesignModel;

namespace API.Model.MasterGemstoneModel
{
    public class ReponseMasterGemstone
    {
        public int MasterGemstoneId { get; set; }

        public string Kind { get; set; } = null!;

        public decimal? Size { get; set; }

        public decimal? Price { get; set; }

        public string? Clarity { get; set; }

        public string? Cut { get; set; }

        public decimal? Weight { get; set; }

        public string? Shape { get; set; }

        public string Image { get; set; }

        public virtual ICollection<RequestCreateDesignModel> Designs { get; set; } = new List<RequestCreateDesignModel>();
    }
}
