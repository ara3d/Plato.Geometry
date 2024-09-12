using System.Collections.Generic;
using System.Linq;
using Ara3D.Collections;
using Ara3D.Geometry;
using Assets.ClonerExample;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets
{
    [ExecuteAlways]
    public class CityBlock : MonoBehaviour
    {
        public CloneAppearance BuldingAppearance = new CloneAppearance();
        public CloneAppearance BlockAppearance = new CloneAppearance();
        public CloneAppearance BetweenStoryAppearance = new CloneAppearance();

        public Material Material;

        public float BlockSize = 60;
        public float SideWalkSize = 1;
        public float RoadSize = 8;
        public float MinStoryHeight = 3f;
        public float MaxStoryHeight = 5f;
        public int MaxStories = 20;
        public float BuildingSpacing = 0.5f;
        public bool Recompute = false;
        public int GridSize = 5;
        public Mesh ClonedMesh;
        public int MinBuildingsPerSide = 2;
        public int MaxBuildingsPerSide = 5;
        public ulong Seed = 41222;
        public float GroundHeight = 0.25f;
        public float StorySepHeight = 0.1f;

        public Clones BuildingClones;
        public Clones BlockClones;
        public Clones BetweenStoryClones;

        public int CloneCount;
        //public Material RoadMaterial;
        //public Box RoadsBox; 

        public float TotalBlockSize => BlockSize + RoadSize + SideWalkSize * 2; 

        public void OnValidate()
        {
            Recompute = true;
        }

        public void OnEnable()
        {
            BuildingClones = new Clones();
            BlockClones = new Clones();
            BetweenStoryClones = new Clones();
        }

        public void OnDisable()
        {
            BuildingClones.Dispose();
            BlockClones.Dispose();
            BetweenStoryClones.Dispose();

            BuildingClones = null;
            BlockClones = null;
            BetweenStoryClones = null;
        }

        public Building CreateBuilding(ulong index, Rect rect)
        {
            var numStories = Rng.GetNthInt(Seed, index, 1, MaxStories);
            var storyHeight = Rng.GetNthFloat(Seed + 1, index, MinStoryHeight, MaxStoryHeight);
            var stories = new List<Story>();
            var offset = GroundHeight;
            for (var i = 0; i < numStories; i++)
            {
                stories.Add(new Story(rect.ToBox(storyHeight, offset)));
                offset += storyHeight;
                if (i < numStories - 1)
                {
                    stories.Add(new Story(rect.ToBox(StorySepHeight, offset)));
                    offset += StorySepHeight;
                }
            }

            return new Building(stories);
        }

        public Block CreateBlock(ulong index, Rect rect)
        {
            rect = rect.Shrink(RoadSize);
            var blockGround = new Box(rect, GroundHeight, 0);
            rect = rect.ShrinkFromCenter(SideWalkSize);

            var numCols = Rng.GetNthInt(Seed, index * 2, MinBuildingsPerSide, MaxBuildingsPerSide + 1);
            var numRows = Rng.GetNthInt(Seed, index * 2 + 1, MinBuildingsPerSide, MaxBuildingsPerSide + 1);
            var cols = (numCols + 1).InterpolateInclusive();
            var rows = (numRows + 1).InterpolateInclusive();
            rect = rect.Grow(BuildingSpacing);
            var lots = rect.Subdivide(cols, rows);
            var bldgs = lots.Select((r, i) => CreateBuilding(index * 7777 + (ulong)i, r.Shrink(BuildingSpacing)));
            return new Block(blockGround, bldgs);
        }

        public void UpdateClones(Clones clones, CloneAppearance appearance, IReadOnlyList<Box> boxes)
        {
            clones.Update(ClonedMesh, Material, boxes.Count, boxes.Select(box => FromBox(appearance, box)));
        }

        public void ComputeGeometry()
        {
            var blockRect = new Rect(0, 0, TotalBlockSize, TotalBlockSize);
            var blocks = blockRect.Repeat(GridSize, GridSize).Select((r, i) => CreateBlock((ulong)i,r)).ToList();
            var mainStories = blocks.SelectMany(b => b.Buildings).SelectMany(b => b.MainStories.Select(s => s.Box)).ToList();
            var betweenStories = blocks.SelectMany(b => b.Buildings).SelectMany(b => b.BetweenStories.Select(s => s.Box)).ToList();
            var sidewalks = blocks.Select(b => b.SidewalkBox).ToList();
            UpdateClones(BuildingClones, BuldingAppearance, mainStories);
            UpdateClones(BlockClones, BlockAppearance, sidewalks);
            UpdateClones(BetweenStoryClones, BetweenStoryAppearance, betweenStories);
            CloneCount = mainStories.Count + sidewalks.Count;
        }

        public static GpuInstanceData FromBox(CloneAppearance appearance, Box box)
        {
            return new GpuInstanceData()
            {
                Color = appearance.Color.ToFloat4(),
                Orientation = quaternion.identity,
                Pos = new float3(box.Rect.center.x, box.Height / 2 + box.Offset, box.Rect.center.y),
                Metallic = appearance.Metallic,
                Smoothness = appearance.Smoothness,
                Scl = new float3(box.Rect.width, box.Height, box.Rect.height),
            };
        }

        // Update is called once per frame
        void Update()
        {
            if (BlockClones == null || !BlockClones.IsValid)
                Recompute = true;

            if (BuildingClones == null || !BuildingClones.IsValid)
                Recompute = true;

            if (Recompute)
            {
                ComputeGeometry();
                Recompute = false; 
            }

            BlockClones.RenderData.Render(ShadowCastingMode.On, true);
            BuildingClones.RenderData.Render(ShadowCastingMode.On, true);
            BetweenStoryClones.RenderData.Render(ShadowCastingMode.On, true);
        }
    }

    public class Box
    {
        public readonly Rect Rect;
        public readonly float Height;
        public readonly float Offset;

        public Box(Rect rect, float height, float offset)
            => (Rect, Height, Offset) = (rect, height, offset);
    }

    public class Block
    {
        public readonly Box SidewalkBox;
        public readonly IReadOnlyList<Building> Buildings;

        public Block(Box ground, IEnumerable<Building> buildings)
            => (SidewalkBox, Buildings) = (ground, buildings.ToList());
    }

    public class Story
    {
        public readonly Box Box;

        public Story(Box box)
            => Box = box;
    }

    public class Building
    {
        public readonly IReadOnlyList<Story> Stories;
        public IEnumerable<Story> MainStories => Stories.Where((_, i) => i % 2 == 0);
        public IEnumerable<Story> BetweenStories => Stories.Where((_, i) => i % 2 == 1);

        public Building(IReadOnlyList<Story> stories)
            => Stories = stories.ToList();
    }

    public static class RectExtensions
    {
        public static Vector2 Lerp(this Vector2 a, Vector2 b, Vector2 amount)
            => new (Mathf.Lerp(a.x, b.x, amount.x), Mathf.Lerp(a.y, b.y, amount.y));

        public static Vector2 Lerp(this Rect rect, Vector2 uvMin)
            => rect.min.Lerp(rect.max, uvMin);

        public static Rect Shrink(this Rect rect, float amount)
            => rect.Shrink(new Vector2(amount, amount));

        public static Rect ShrinkFromCenter(this Rect rect, float amount)
            => rect.Shrink(amount * 2).Offset(new Vector2(amount, amount));

        public static Rect GrowFromCenter(this Rect rect, float amount)
            => rect.ShrinkFromCenter(-amount);

        public static Rect Offset(this Rect rect, Vector2 amount)
            => new Rect(rect.position + amount, rect.size);

        public static Rect Shrink(this Rect rect, Vector2 amount)
            => rect.min.Rect(rect.max - amount);

        public static Rect Grow(this Rect rect, Vector2 amount)
            => rect.Shrink(-amount);

        public static Rect Grow(this Rect rect, float amount)
            => rect.Shrink(-amount);

        public static IReadOnlyList<Rect> Subdivide(this Rect rect, Vector2 uvCenter)
            => new[]
            {
                rect.Lerp(Vector2.zero, uvCenter),
                rect.Lerp(new Vector2(0, uvCenter.y), new Vector2(uvCenter.x, 1)),
                rect.Lerp(new Vector2(uvCenter.x, 0), new Vector2(1, uvCenter.y)),
                rect.Lerp(uvCenter, Vector2.one),
            };

        public static Vector2 Average(this Vector2 a, Vector2 b)
            => (a + b) / 2;

        public static Rect Rect(this Vector2 from, Vector2 upTo)
            => new(from.x, from.y, upTo.x - from.x, upTo.y - from.y);

        public static Rect Lerp(this Rect rect, Vector2 uvMin, Vector2 uvMax)
            => rect.Lerp(uvMin).Rect(rect.Lerp(uvMax));

        public static IEnumerable<Rect> Subdivide(this Rect rect, int cols, int rows)
            => rect.Subdivide((cols + 1).InterpolateInclusive(), (rows + 1).InterpolateInclusive());

        public static IEnumerable<Rect> Subdivide(this Rect rect, int cols, int rows, float spacing)
            => rect.Grow(spacing).Subdivide(cols, rows)
                .Select(r => r.Shrink(spacing));

        public static Box ToBox(this Rect rect, float height = 1f, float offset = 0f)
            => new Box(rect, height, offset); 

        public static IEnumerable<Rect> Subdivide(this Rect rect, IArray<float> xs, IArray<float> ys)
        {
            for (var j = 0; j < ys.Count - 1; j++)
            {
                for (var i = 0; i < xs.Count - 1; i++)
                {
                    var min = new Vector2(xs[i], ys[j]);
                    var max = new Vector2(xs[i + 1], ys[j+1]);
                    var tmp = rect.Lerp(min, max);
                    yield return tmp;
                }
            }
        }

        public static Rect Multiply(this Rect rect, float x, float y)
            => new(rect.min.x, rect.min.y, rect.size.x * x, rect.size.y * y);

        public static IEnumerable<Rect> Repeat(this Rect rect, int cols, int rows)
            => rect.Multiply(cols, rows).Subdivide(cols, rows);
    }
}