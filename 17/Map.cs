using System;
using System.Linq;
using System.Collections.Generic;

namespace _17
{

    public class Map
    {
        protected Dictionary<long, Dictionary<long, Dictionary<long, bool>>> data = new Dictionary<long, Dictionary<long, Dictionary<long, bool>>>();

        public class SubMapY
        {
            private long x, y;
            private Map map;
            public SubMapY(Map map, long x, long y)
            {
                this.x = x;
                this.y = y;
                this.map = map;
            }

            public bool this[long z]
            {
                get => map.data.ContainsKey(x) ? map.data[x].ContainsKey(y) ? map.data[x][y].ContainsKey(z) ? map.data[x][y][z] : false : false : false;
                set
                {
                    if (!map.data.ContainsKey(x))
                    {
                        map.data[x] = new Dictionary<long, Dictionary<long, bool>>();
                    }

                    if (!map.data[x].ContainsKey(y))
                    {
                        map.data[x][y] = new Dictionary<long, bool>();
                    }

                    map.data[x][y][z] = value;
                }
            }
        }

        public class SubMapX
        {
            private readonly Map map;
            private readonly long x;
            public SubMapX(Map map, long x)
            {
                this.x = x;
                this.map = map;
            }

            public SubMapY this[long y]
            {
                get => new SubMapY(map, x, y);
            }
        }

        public SubMapX this[long x]
        {
            get => new SubMapX(this, x);
        }

        public int CountActiveNeighbors(long x, long y, long z)
        {
            int result = 0;
            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var j = y - 1; j <= y + 1; j++)
                {
                    for (var k = z - 1; k <= z + 1; k++)
                    {
                        if (!(x == i && y == j && z == k) && this[i][j][k])
                        {
                            result++;
                        }
                    }
                }
            }

            return result;
        }

        public bool IsActive(long x, long y, long z)
        {
            return data.ContainsKey(x) && data[x].ContainsKey(y) && data[x][y].ContainsKey(z) && data[x][y][z];
        }

        public long MinX => this.data.Keys.Min();
        public long MaxX => this.data.Keys.Max();
        public long MinY => this.data[0].Keys.Min();
        public long MaxY => this.data[0].Keys.Max();
        public long MinZ => this.data[0].Keys.Min();
        public long MaxZ => this.data[0].Keys.Max();

        public long CountActive()
        {
            return data.Sum(i =>
                i.Value.Sum(j =>
                    j.Value.Count(x => x.Value == true)
                )
            );
        }
    }
}