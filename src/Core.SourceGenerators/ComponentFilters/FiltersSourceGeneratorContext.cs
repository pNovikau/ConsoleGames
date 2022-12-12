namespace Core.SourceGenerators.ComponentFilters
{
    public static class FiltersSourceGeneratorContext
    {
        public static ComponentFiltersContentBuilder FilterBuilder;
        public static ComponentsTuplesContentBuilder TuplesBuilder;

        public static void Initialize()
        {
            FilterBuilder = new ComponentFiltersContentBuilder();
            TuplesBuilder = new ComponentsTuplesContentBuilder();
        }

        public static void Clear()
        {
            FilterBuilder = null;
            TuplesBuilder = null;
        }
    }
}