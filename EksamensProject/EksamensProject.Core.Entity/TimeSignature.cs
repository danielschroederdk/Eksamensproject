using System.ComponentModel.DataAnnotations;

namespace EksamensProject.Core.Entity
{
    public enum TimeSignature
    {
        [Display(Name = "4/4")]
        FourFour,
        [Display(Name = "2/2")]
        TwoTwo,
        [Display(Name = "2/4")] 
        TwoFour, 
        [Display(Name = "3/4")]
        ThreeFour,
        [Display(Name = "3/8")]
        ThreeAight,
        [Display(Name = "6/8")]
        SixAight,
        [Display(Name = "9/8")]
        NineAight,
        [Display(Name = "12/8")]
        TwelveAight
    }
}