namespace Matrix
{
	public class Point
	{
		public int X { get; set; }
		public int Y { get; set; }
		public bool Recycled { get; set; }

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}
