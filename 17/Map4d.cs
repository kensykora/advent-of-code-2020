using System;
using System.Linq;
using System.Collections.Generic;

namespace _17
{

    public class Map4d
    {
        protected Dictionary<long, Dictionary<long, Dictionary<long, Dictionary<long, bool>>>> data = new Dictionary<long, Dictionary<long, Dictionary<long, Dictionary<long, bool>>>>();

        public class SubMapZ
        {
            private long x, y, z;
            private Map4d map;

            public SubMapZ(Map4d map, long x, long y, long z)
            {
                this.x = x;
                this.y = y;
                this.z = z;

                this.map = map;
            }

            public bool this[long q]
            {
                get => map.data.ContainsKey(x) ? map.data[x].ContainsKey(y) ? map.data[x][y].ContainsKey(z) ? map.data[x][y][z].ContainsKey(q) ? map.data[x][y][z][q] : false : false : false : false;
                set
                {
                    if (!map.data.ContainsKey(x))
                    {
                        map.data[x] = new Dictionary<long, Dictionary<long, Dictionary<long, bool>>>();
                    }

                    if (!map.data[x].ContainsKey(y))
                    {
                        map.data[x][y] = new Dictionary<long, Dictionary<long, bool>>();
                    }

                    if (!map.data[x][y].ContainsKey(z))
                    {
                        map.data[x][y][z] = new Dictionary<long, bool>();
                    }

                    map.data[x][y][z][q] = value;
                }
            }
        }

        public class SubMapY
        {
            private long x, y;
            private Map4d map;
            public SubMapY(Map4d map, long x, long y)
            {
                this.x = x;
                this.y = y;
                this.map = map;
            }

            public SubMapZ this[long z]
            {
                get => new SubMapZ(map, x, y, z);
            }
        }

        public class SubMapX
        {
            private readonly Map4d map;
            private readonly long x;
            public SubMapX(Map4d map, long x)
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

        public int CountActiveNeighbors(long x, long y, long z, long q)
        {
            int result = 0;
            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var j = y - 1; j <= y + 1; j++)
                {
                    for (var k = z - 1; k <= z + 1; k++)
                    {
                        for(var l = q - 1; l <= q + 1; l++)
                        {
                            if (!(x == i && y == j && z == k && q == l) && this[i][j][k][l])
                            {
                                result++;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public bool IsActive(long x, long y, long z, long q)
        {
            return data.ContainsKey(x) && data[x].ContainsKey(y) && data[x][y].ContainsKey(z) && data[x][y][z].ContainsKey(q) && data[x][y][z][q];
        }

        public long MinX => this.data.Keys.Min();
        public long MaxX => this.data.Keys.Max();
        public long MinY => this.data[0].Keys.Min();
        public long MaxY => this.data[0].Keys.Max();
        public long MinZ => this.data[0].Keys.Min();
        public long MaxZ => this.data[0].Keys.Max();
        public long MinQ => this.data[0].Keys.Min();
        public long MaxQ => this.data[0].Keys.Max();

        public long CountActive()
        {
            return data.Sum(i =>
                i.Value.Sum(j =>
                j.Value.Sum(k =>
                    k.Value.Count(x => x.Value == true)
                ))
            );
        }
    }
}