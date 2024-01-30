namespace Portfolio.API.Common.Models
{
    public class SkillModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? RateOutOfFive { get; set;}
    }
}
