using Backend.Models.Backend.StockModel;

namespace Backend.Models.Backend
{
    public class ArchiveStock : StockBase
    {
        public Dictionary<string, ArchiveData> Data { get; set; } = new Dictionary<string, ArchiveData>();

        public bool IsUpdated { get; set; } = false;

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
                sum += 1 / volum.Value.Volume;
            }

            return Data.Count / sum;
        }

        private double ArifmeticCalculation()
        {
            var sum = 0.0;

            foreach (var volum in Data)
            {
                sum += volum.Value.Volume;
            }

            return Data.Count / sum;
        }

        private double SquareCalculation()
        {
            var sum = 0.0;

            foreach (var volum in Data)
            {
                sum += volum.Value.Volume * volum.Value.Volume;
            }

            return Math.Sqrt(Data.Count / sum);
        }
    }

}
