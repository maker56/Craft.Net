
using Craft.Net.Data.Blocks;
namespace Craft.Net.Data.Generation
{
    /// <summary>
    /// Generates a world that mimics the Minecraft flatland generator
    /// with structures turned off.
    /// </summary>
    public class FlatlandGenerator : IWorldGenerator
    {
        static FlatlandGenerator()
        {
            DefaultGeneratorOptions = "1;7,2x3,2;1";
        }

        public static string DefaultGeneratorOptions { get; set; }

        public string GeneratorOptions { get; set; }

        public void Initialize(Level level)
        {
            if (level.GeneratorOptions == null)
                GeneratorOptions = DefaultGeneratorOptions;
            else
                GeneratorOptions = level.GeneratorOptions;
        }

        public Chunk GenerateChunk(Vector3 position, Region parentRegion)
        {
            var chunk = GenerateChunk(position);
            chunk.ParentRegion = parentRegion;
            return chunk;
        }

        public Chunk GenerateChunk(Vector3 position)
        {
            var chunk = new Chunk(position);
            for (int x = 0; x < 16; x++)
            {
                for (int z = 0; z < 16; z++)
                {
                    for (int y = 1; y < 3; y++)
                        chunk.SetBlock(new Vector3(x, y, z), new DirtBlock());
                    chunk.SetBlock(new Vector3(x, 0, z), new BedrockBlock());
                    chunk.SetBlock(new Vector3(x, 3, z), new GrassBlock());
                    chunk.SetBiome((byte)x, (byte)z, Biome.Plains);
                }
            }
            return chunk;
        }

        public string LevelType
        {
            get { return "FLAT"; }
        }

        public string GeneratorName { get { return "FLAT"; } }

        public long Seed { get; set; }

        public Vector3 SpawnPoint
        {
            get { return new Vector3(0, 4, 0); }
        }
    }
}