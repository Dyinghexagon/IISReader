namespace Backend.Models.Backend.StockModel
{
    public class ArchiveStock : StockBase
    {

        public Dictionary<string, double> Volumes { get; set; } = new Dictionary<string, double>();

        public double GetVolume(CalculationType calculationType)
        {
            switch (calculationType)
            {
                case CalculationType.Hormonic:
                    {
                        return HormonicCalculation();
                    }
                case CalculationType.Arifmetic:
                    {
                        return ArifmeticCalculation();
                    }
                case CalculationType.Square:
                    {
                        return SquareCalculation();
                    }
            }

            return double.MinValue;
        }


        private double HormonicCalculation()
        {
            var sum = 0.0;

            foreach (var volum in Volumes)
            {
                sum += 1 / volum.Value;
            }

            return Volumes.Count / sum;
        }

        private double ArifmeticCalculation()
        {
            var sum = 0.0;

            foreach (var volum in Volumes)
            {
                sum += volum.Value;
            }

            return Volumes.Count / sum;
        }

        private double SquareCalculation()
        {
            var sum = 0.0;

            foreach (var volum in Volumes)
            {
                sum += volum.Value * volum.Value;
            }

            return Math.Sqrt(Volumes.Count / sum);
        }

    }
}
