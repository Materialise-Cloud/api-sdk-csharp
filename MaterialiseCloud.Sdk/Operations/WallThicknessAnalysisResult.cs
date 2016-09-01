namespace MaterialiseCloud.Sdk.Operations
{
    public class WallThicknessAnalysisResult
    {
        public string FileId { get; set; }
        public bool HasThinWalls { get; set; }
        public bool HasPossibleThinWalls { get; set; }
    }
}