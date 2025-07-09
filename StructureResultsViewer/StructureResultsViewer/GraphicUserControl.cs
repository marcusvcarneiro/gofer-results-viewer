using GoferAnalysisDTOs.Results;
using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.DataVisualization.Charting;

namespace StructureResultsViewer
{
    public partial class GraphicUserControl : UserControlBase
    {
        private double x0;
        private double y0;
        private double z0;
        private float rotation;
        private string _selected_result;
        private string _selected_direction;


        public GraphicUserControl()
        {
            InitializeComponent();
            x0 = 0; 
            y0 = 0; 
            z0 = 20;
            rotation = 0.0f;
            _selected_result = string.Empty;
            _selected_direction = string.Empty;
        }

        private void PlotResults()
        {
            if (ModelController == null)
            {
                return;
            }
            if (ModelController.Results == null)
            {
                return;
            }
            if (ModelController.Results.StructureElements.Count == 0)
            {
                MessageBox.Show("The input json does not contain structure elements. Try another. ", "Nothing to show", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //PlotElementResults()
        }

        private double GetValue(Displacement displacement, string direction)
        {
            if (direction == "X")
                return displacement.X;
            else if (direction == "Y")
                return displacement.Y;
            else
                return Math.Sqrt(displacement.X * displacement.X + displacement.Y * displacement.Y);
        }

        //private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    _selected_result = comboBox2.Text;
        //    if (_selected_result == "Bending moment" || _selected_result == "Axial force" || _selected_result == "Shear force")
        //    {
        //        comboBox1.Enabled = false;
        //    }
        //    else
        //    {
        //        comboBox1.Enabled = true;
        //    }
        //    PlotResults();
        //}

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    _selected_direction = comboBox1.Text;
        //    PlotResults();
        //}

        private void openglControl_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    z0 += e.Delta / 50.0;
                    update_view();
                }
                else
                {
                    if (e.Delta > 0)
                    {
                        z0 += e.Delta / 50.0;
                        update_view();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("some error", "err", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void update_view()
        {
            OpenGL gl = openglControl1.OpenGL;

            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            //  Load the identity.
            gl.LoadIdentity();

            //  Create a perspective transformation.
            gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);

            //  Use the 'look at' helper function to position and aim the camera.
            gl.LookAt(x0, y0, z0, 0, 0, 0, 0, 1, 0);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }


        private void openglControl1_Resized(object sender, EventArgs e)
        {
            //  Get the OpenGL object.
            update_view();

        }

        private void openglControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            //  Get the OpenGL object.
            OpenGL gl = openglControl1.OpenGL;

            //  Set the clear color.
            gl.ClearColor(1, 1, 1, 0);

        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {

            if (ModelController == null)
            {
                return;
            }
            if (ModelController.Results == null)
            {
                return;
            }


            //  Get the OpenGL object.
            //  Get the OpenGL object.
            OpenGL gl = openglControl1.OpenGL;

            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Load the identity matrix.
            gl.LoadIdentity();

            Dictionary<Guid, int> guidGsaKeys = new Dictionary<Guid, int>(ModelController.Results.Nodes.Count);

            foreach (Node node in ModelController.Results.Nodes.Values)
            {
                guidGsaKeys.Add(node.Id, node.GsaModelKey);
            }

            foreach (Element2d element2D in ModelController.Results.Elements.Values)
            {
                gl.Color(0.0f, 1.0f, 0.0f);
                gl.Begin(OpenGL.GL_POLYGON);

                List<ElementNode> topo = element2D.Nodes;
                for (int i = 0; i < topo.Count / 2; i++)
                {
                    ElementNode node = topo[i];
                    int gsaModelKey = guidGsaKeys[node.NodeId];
                    double x = ModelController.Results.Nodes[gsaModelKey].X;
                    double y = ModelController.Results.Nodes[gsaModelKey].Y;
                    gl.Vertex(x, y, 0.0f);

                    //node = topo[i + topo.Count / 2];
                    //gsaModelKey = guidGsaKeys[node.NodeId];
                    //x = ModelController.Results.Nodes[gsaModelKey].X;
                    //y = ModelController.Results.Nodes[gsaModelKey].Y;
                    //gl.Vertex(x, y, 0.0f);
                }
                gl.End();

                gl.Begin(OpenGL.GL_LINE_LOOP);
                gl.Color(0.0f, 0.0f, 0.0f);
                for (int i = 0; i < topo.Count / 2; i++)
                {
                    ElementNode node = topo[i];
                    int gsaModelKey = guidGsaKeys[node.NodeId];
                    double x = ModelController.Results.Nodes[gsaModelKey].X;
                    double y = ModelController.Results.Nodes[gsaModelKey].Y;
                    gl.Vertex(x, y, 0.0f);

                    //node = topo[i + topo.Count / 2];
                    //gsaModelKey = guidGsaKeys[node.NodeId];
                    //x = ModelController.Results.Nodes[gsaModelKey].X;
                    //y = ModelController.Results.Nodes[gsaModelKey].Y;
                    //gl.Vertex(x, y, 0.0f);
                }
                gl.End();

                //return;
            }

            //Rotate around the Y axis.
            //gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);
            //double x = 0;
            ////Draw a coloured pyramid.
            //gl.Begin(OpenGL.GL_POLYGON);

            //gl.Color(0.0f, 0.0f, 1.0f);
            //gl.Vertex(x+ 0f, 0f, 0f);

            //gl.Color(0.0f, 1.0f, 0.0f);
            //gl.Vertex(x+10f, 0f, 0f);

            //gl.Color(1.0f, 0.0f, 0.0f);
            //gl.Vertex(x+ 10.0f, 10.0f, 0.0f);

            //gl.Color(1.0f, 0.0f, 0.0f);
            //gl.Vertex(x+ 0.0f, 10.0f, 0.0f);
            //gl.End();


            // x += 15;
            ////Draw a coloured pyramid.
            //gl.Begin(OpenGL.GL_POLYGON);

            //gl.Color(0.0f, 0.0f, 1.0f);
            //gl.Vertex(x + 0f, 0f, 0f);

            //gl.Color(0.0f, 0.0f, 1.0f);
            //gl.Vertex(x + 10f, 0f, 0f);

            //gl.Color(0.0f, 0.0f, 1.0f);
            //gl.Vertex(x + 10.0f, 10.0f, 0.0f);

            //gl.Color(0.0f, 0.0f, 1.0f);
            //gl.Vertex(x + 0.0f, 10.0f, 0.0f);
            //gl.End();
            //gl.Color(0.0f, 0.0f, 1.0f);
            //gl.Vertex(1.0f, -1.0f, 1.0f);
            //gl.Color(0.0f, 1.0f, 0.0f);
            //gl.Vertex(1.0f, -1.0f, -1.0f);
            //gl.Color(1.0f, 0.0f, 0.0f);
            //gl.Vertex(0.0f, 1.0f, 0.0f);
            //gl.Color(0.0f, 1.0f, 0.0f);
            //gl.Vertex(1.0f, -1.0f, -1.0f);
            //gl.Color(0.0f, 0.0f, 1.0f);
            //gl.Vertex(-1.0f, -1.0f, -1.0f);
            //gl.Color(1.0f, 0.0f, 0.0f);
            //gl.Vertex(0.0f, 1.0f, 0.0f);
            //gl.Color(0.0f, 0.0f, 1.0f);
            //gl.Vertex(-1.0f, -1.0f, -1.0f);
            //gl.Color(0.0f, 1.0f, 0.0f);
            //gl.Vertex(-1.0f, -1.0f, 1.0f);


            //  Nudge the rotation.
            rotation += 3.0f;

            //foreach (Element2d element2D in ModelController.Results.Elements.Values)
            //{

            //    gl.Begin(OpenGL.GL_TRIANGLES);

            //    List<ElementNode> topo = element2D.Nodes;
            //    for (int i = 0; i < topo.Count / 2; i++)
            //    {
            //        ElementNode node = topo[i];
            //        int gsaModelKey = guidGsaKeys[node.NodeId];
            //        double x = ModelController.Results.Nodes[gsaModelKey].X;
            //        double y = ModelController.Results.Nodes[gsaModelKey].Y;
            //        gl.Vertex(x, y, 0.0f);

            //        //node = topo[i + topo.Count / 2];
            //        //gsaModelKey = guidGsaKeys[node.NodeId];
            //        //x = ModelController.Results.Nodes[gsaModelKey].X;
            //        //y = ModelController.Results.Nodes[gsaModelKey].Y;
            //        //gl.Vertex(x, y, 0.0f);
            //    }
            //    gl.End();

            //    return;
            //}


            //for


        }
    }
}
