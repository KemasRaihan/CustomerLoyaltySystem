using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Models
{
    public class Reward
    {

        [Display(Name = "Reward ID")]
        public int rewardId { get; set; }
        [Display(Name = "Name")]
        public string rewardName { get; set; }
        [Display(Name = "Points Required")]
        public int pointsToClaim { get; set; }
        [Display(Name = "Reward Description")]
        public string rewardDescription { get; set; }
        public string rewardImageSrc { get; set; }
    }
}