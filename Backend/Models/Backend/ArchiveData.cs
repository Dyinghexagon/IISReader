using Backend.Models.Backend.StockModel;

namespace Backend.Models.Backend
{
    public class ArchiveData : StockBase
    {
        public Dictionary<string, ArchiveStock> Data { get; set; } = new Dictionary<string, ArchiveStock>();

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

            foreach (var volum in Data)
            {
                sum += 1 / volum.Value.Volumn;
            }

            return Data.Count / sum;
        }

        private double ArifmeticCalculation()
        {
            var sum = 0.0;

            foreach (var volum in Data)
            {
                sum += volum.Value.Volumn;
            }

            return Data.Count / sum;
        }

        private double SquareCalculation()
        {
            var sum = 0.0;

            foreach (var volum in Data)
            {
                sum += volum.Value.Volumn * volum.Value.Volumn;
            }

            return Math.Sqrt(Data.Count / sum);
        }
    }

}
