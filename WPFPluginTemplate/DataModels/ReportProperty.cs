using Tekla.Structures.Model;

namespace Trap2_0.DataModels
{
    public class ReportProperty
    {
        public double BreedtePofile { get; set; }
        public double HoogteProfile { get; set; }
        public double lijfDikteProfile { get; set; }
        public double FlensDikteProfile { get; set; }
        public double RondingsRadiusProfile { get; set; }
        public string ProfileType { get; set; }
        public double ProfileHellingVerhouding { get; set; }
        public double plaatdikte { get; set; }

        public ReportProperty(Beam Profile)
        {
            var ProfileWith = 0.0;
            var ProfileHeight = 0.0;
            var lijfdikteProfile = 0.0;
            var dikteflensProfile = 0.0;
            var rondingRadiusProfile = 0.0;
            var profileType = "";
            var profileHellingVerhouding = 0.0;
            var plaatDikte = 0.0;

            Profile.GetReportProperty("WIDTH", ref ProfileWith);
            Profile.GetReportProperty("HEIGHT", ref ProfileHeight);
            Profile.GetReportProperty("PROFILE.WEB_THICKNESS", ref lijfdikteProfile);
            Profile.GetReportProperty("PROFILE.FLANGE_THICKNESS", ref dikteflensProfile);
            Profile.GetReportProperty("PROFILE.ROUNDING_RADIUS_1", ref rondingRadiusProfile);
            Profile.GetReportProperty("PROFILE_TYPE", ref profileType);
            Profile.GetReportProperty("PROFILE.FLANGE_SLOPE_RATIO", ref profileHellingVerhouding);
            Profile.GetReportProperty("PROFILE.PLATE_THICKNESS", ref plaatDikte);

            BreedtePofile = ProfileWith;
            HoogteProfile = ProfileHeight;
            lijfDikteProfile = lijfdikteProfile;
            FlensDikteProfile = dikteflensProfile;
            RondingsRadiusProfile = rondingRadiusProfile;
            ProfileType = profileType;
            ProfileHellingVerhouding = profileHellingVerhouding;
            plaatdikte = plaatDikte;
        }
    }
}
