namespace SLNW
{
    public struct NerualOutputData
    {
        public readonly int arraySize;
        public readonly double[] data;

        public NerualOutputData(int size)
        {
            arraySize = size;

            data = new double[size];
        }
    }
}
