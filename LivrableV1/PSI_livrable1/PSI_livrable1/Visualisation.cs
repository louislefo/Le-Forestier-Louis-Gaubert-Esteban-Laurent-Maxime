using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PSI_livrable1
{
    internal class Visualisation
    {
        private List<(int, int)> edges;
        private Dictionary<int, PointF> positions;
        private int imageSize = 600;

        public Visualisation(List<(int, int)> edges)
        {
            this.edges = edges;
            this.positions = new Dictionary<int, PointF>();
        }

        private void ComputePositions()
        {
            Random rand = new Random();
            HashSet<int> nodes = new HashSet<int>();

            foreach (var edge in edges)
            {
                nodes.Add(edge.Item1);
                nodes.Add(edge.Item2);
            }

            int count = nodes.Count;
            int i = 0;
            foreach (int node in nodes)
            {
                float angle = (float)(2 * Math.PI * i / count);
                float x = imageSize / 2 + (float)(Math.Cos(angle) * imageSize / 3);
                float y = imageSize / 2 + (float)(Math.Sin(angle) * imageSize / 3);
                positions[node] = new PointF(x, y);
                i++;
            }
        }

        public void DrawGraph(string filename)
        {
            ComputePositions();

            using (Bitmap bitmap = new Bitmap(imageSize, imageSize))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                Pen edgePen = new Pen(Color.Black, 2);
                Brush nodeBrush = new SolidBrush(Color.SkyBlue);
                System.Drawing.Font font = new System.Drawing.Font("Arial", 12);
                Brush textBrush = new SolidBrush(Color.Black);
                int nodeRadius = 20;

                foreach (var edge in edges)
                {
                    PointF p1 = positions[edge.Item1];
                    PointF p2 = positions[edge.Item2];
                    g.DrawLine(edgePen, p1, p2);
                }

                foreach (var node in positions)
                {
                    PointF p = node.Value;
                    g.FillEllipse(nodeBrush, p.X - nodeRadius, p.Y - nodeRadius, nodeRadius * 2, nodeRadius * 2);
                    g.DrawString(node.Key.ToString(), font, textBrush, p.X - 8, p.Y - 8);
                }

                bitmap.Save(filename, ImageFormat.Png);
                //ZIZI
            }

            Console.WriteLine("Graph saved as " + filename);
        }
    }
}
