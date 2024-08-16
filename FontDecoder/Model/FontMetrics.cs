using SharpFont;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontDecoder.Model
{    
    public class FontMetrics
    {
        public char Character {  get; set; }
        public PointF[] Points { get; set; }
        public int[] Contours {  get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public FontMetrics() { }
        public FontMetrics(char character, Glyph glyph)
        {
            Character = character;
            Points = glyph.points;
            Contours = glyph.contours;
            Height = glyph.Height;
            Width = glyph.Width;
        }
        public FontMetrics(Glyph glyph)
        {
            Points = glyph.points;
            Contours = glyph.contours;
            Height = glyph.Height;
            Width = glyph.Width;
        }
        public override bool Equals(object? obj)
        {
            if (!(obj is FontMetrics))
                return false;

            var other = obj as FontMetrics;

            if (other.Points.Length != this.Points.Length || other.Contours.Length != this.Contours.Length)
            {
                return false;
            }

            int i = 0;
            for (; i < Contours.Length; i++)
            {
                if (other.Contours[i] != Contours[i])
                    return false;
            }

            for (i = 0; i < Points.Length; i++)
            {
                if (!other.Points[i].Equals(Points[i]))
                    return false;
            }

            return other.Height == Height && other.Width == Width;
        }
    }
}
