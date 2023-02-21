using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {

        List<Button> buttons = new List<Button>();
        int objectFormat = 0;
        int rotation = 0;
        List<Color> colors = new List<Color> { Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Orange };
        //
        List<int> xMap = new List<int>();
        List<int> yMap = new List<int>();
        int deslocamento = 20;
        int tamanho = 20;
        int xCount = 0;
        int yCount = 0;
        bool[,] mapa2D = new bool[49, 90];//L, C

        public Form1()
        {
            InitializeComponent();
        }

        private void create(int x, int y, string text = "")
        {
            Button button = new Button();
            button.BackColor = colors[objectFormat];//
            button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            button.Location = new System.Drawing.Point(x, y);//X, Y
            button.Name = "buttonX";
            button.Text = text;
            button.Size = new System.Drawing.Size(tamanho, tamanho);
            //button.TabIndex = 0;
            button.UseVisualStyleBackColor = false;
            this.Controls.Add(button);
            buttons.Add(button);
        }

        private void part01(int format_)
        {
            objectFormat = format_;
            switch (format_)
            {
                case 0:
                    create(0, 0, "0");
                    create(20, 0, "1");
                    create(40, 0, "2");
                    create(20, 20, "3");
                    break;
                case 1:
                    create(0, 0, "0");
                    create(0, 20, "1");
                    create(20, 20, "2");
                    create(20, 40, "3");
                    break;
                case 2:
                    create(0, 0, "0");
                    create(0, 20, "1");
                    create(0, 40, "2");
                    create(20, 40, "3");
                    break;
                case 3:
                    create(0, 0, "0");
                    create(20, 0, "1");
                    create(40, 0, "2");
                    create(60, 0, "3");
                    break;
                case 4:
                    create(0, 0, "0");
                    create(0, 20, "1");
                    create(20, 0, "2");
                    create(20, 20, "3");
                    break;
                    //case 5:
                    //    create(0, 0, "0");
                    //    break;
                    //case 6:
                    //    create(0, 0, "0");
                    //    create(deslocamento, 0, "1");
                    //    break;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (buttons.Count > 0)
            {
                int limiteInferior = this.Size.Height - 68;
                bool valid = false;
                for (int i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i].Location.Y/* + deslocamento*/ < limiteInferior && mapa2D[(buttons[i].Location.Y / deslocamento), (buttons[i].Location.X / deslocamento)] == false)
                    {
                        valid = true;
                    }
                    else
                    {

                    }
                }
                if (valid)
                {
                    switch (buttons.Count)
                    {
                        case 1:
                            buttons[0].Location = new Point(buttons[0].Location.X, buttons[0].Location.Y + deslocamento);
                            break;
                        case 2:
                            if (mapa2D[(buttons[0].Location.Y / deslocamento), (buttons[0].Location.X / deslocamento)] == false && mapa2D[(buttons[1].Location.Y / deslocamento), (buttons[1].Location.X / deslocamento)] == false)
                            {
                                buttons[0].Location = new Point(buttons[0].Location.X, buttons[0].Location.Y + deslocamento);
                                buttons[1].Location = new Point(buttons[1].Location.X, buttons[1].Location.Y + deslocamento);
                            }
                            else
                            {
                                novePeca();
                            }
                            break;
                        case 4:
                            try
                            {
                                if (mapa2D[(buttons[0].Location.Y / deslocamento), (buttons[0].Location.X / deslocamento)] == false &&
                                mapa2D[(buttons[1].Location.Y / deslocamento), (buttons[1].Location.X / deslocamento)] == false &&
                                mapa2D[(buttons[2].Location.Y / deslocamento), (buttons[2].Location.X / deslocamento)] == false &&
                                mapa2D[(buttons[3].Location.Y / deslocamento), (buttons[3].Location.X / deslocamento)] == false)
                                {
                                    buttons[0].Location = new Point(buttons[0].Location.X, buttons[0].Location.Y + deslocamento);
                                    buttons[1].Location = new Point(buttons[1].Location.X, buttons[1].Location.Y + deslocamento);
                                    buttons[2].Location = new Point(buttons[2].Location.X, buttons[2].Location.Y + deslocamento);
                                    buttons[3].Location = new Point(buttons[3].Location.X, buttons[3].Location.Y + deslocamento);
                                }
                                else
                                {
                                    novePeca();
                                }
                            }
                            catch (Exception)
                            {
                                novePeca();
                            }
                            break;
                    }
                }
                else
                {
                    novePeca();
                }
            }
            bool gameOver = false;
            for (int i = 0; i < 90; i++)
            {
                if (mapa2D[0, i] == true)
                {
                    gameOver = true;
                    break;
                }
            }
            if (gameOver)
            {
                timer.Enabled = false;
                MessageBox.Show("Game Over!!!");
            }
        }

        private void novePeca()
        {
            //
            for (int i = 0; i < buttons.Count; i++)
            {
                int linha = (buttons[i].Location.Y / deslocamento);
                int coluna = (buttons[i].Location.X / deslocamento);
                linha = linha == 0 ? linha : linha - 1;
                mapa2D[linha, coluna] = true;
            }
            //
            buttons.Clear();
            Random random = new Random();
            part01(random.Next(0, 5));
            rotation = 0;
            //part01(3);//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< DESCOMENTAR PARA VISUALIZAR UMA NOVA PEÇA APÓS A ATUAL CHEGAR NO CHÃO
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 90; i++)//colunas
            {
                xMap.Add(xCount);
                xCount += deslocamento;
            }
            for (int i = 0; i < 49; i++)//linhas
            {
                yMap.Add(yCount);
                yCount += deslocamento;
            }
            //MessageBox.Show($"{this.Size.Height}:{this.Size.Width}");//1048:1936
            //create();
            Random random = new Random();
            part01(random.Next(0, 5));
            //part01(3);//CRIA UMA PEÇA NA INICIALIZAÇÃO
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            string key = e.KeyCode.ToString().ToLower();
            if (key.Equals("a") || key.Equals("d") || key.Equals("s"))
            {
                move(key);
            }
            if (key.Equals("space"))
            {
                rotate();
            }
        }

        private void rotate()
        {
            switch (objectFormat)
            {
                case 0:
                    switch (rotation)
                    {
                        case 0:
                            buttons[2].Location = new Point(buttons[2].Location.X - deslocamento, buttons[2].Location.Y - deslocamento);
                            break;
                        case 1:
                            buttons[3].Location = new Point(buttons[3].Location.X + deslocamento, buttons[3].Location.Y - deslocamento);
                            break;
                        case 2:
                            buttons[2].Location = new Point(buttons[2].Location.X - deslocamento, buttons[2].Location.Y);
                            buttons[3].Location = new Point(buttons[3].Location.X - 40, buttons[3].Location.Y + deslocamento);
                            break;
                        case 3:
                            buttons[2].Location = new Point(buttons[2].Location.X + 40, buttons[2].Location.Y + deslocamento);
                            buttons[3].Location = new Point(buttons[3].Location.X + deslocamento, buttons[3].Location.Y);
                            break;
                    }
                    rotation = rotation < 3 ? rotation + 1 : 0;
                    break;
                case 1:
                    switch (rotation)
                    {
                        case 0:
                            buttons[0].Location = new Point(buttons[0].Location.X + deslocamento, buttons[0].Location.Y);
                            buttons[3].Location = new Point(buttons[3].Location.X + deslocamento, buttons[3].Location.Y - 40);
                            break;
                        case 1:
                            buttons[0].Location = new Point(buttons[0].Location.X - deslocamento, buttons[0].Location.Y);
                            buttons[3].Location = new Point(buttons[3].Location.X - deslocamento, buttons[3].Location.Y + 40);
                            break;
                    }
                    rotation = rotation < 1 ? rotation + 1 : 0;
                    break;
                case 2:
                    switch (rotation)
                    {
                        case 0:
                            buttons[2].Location = new Point(buttons[2].Location.X + deslocamento, buttons[2].Location.Y - 40);
                            buttons[3].Location = new Point(buttons[3].Location.X + deslocamento, buttons[3].Location.Y - 40);
                            break;
                        case 1:
                            buttons[1].Location = new Point(buttons[1].Location.X + deslocamento, buttons[1].Location.Y);
                            buttons[3].Location = new Point(buttons[3].Location.X - deslocamento, buttons[3].Location.Y + 40);
                            break;
                        case 2:
                            buttons[1].Location = new Point(buttons[1].Location.X + deslocamento, buttons[1].Location.Y - deslocamento);
                            buttons[3].Location = new Point(buttons[3].Location.X + deslocamento, buttons[3].Location.Y - 60);
                            break;
                        case 3:
                            buttons[1].Location = new Point(buttons[1].Location.X - 40, buttons[1].Location.Y + deslocamento);
                            buttons[2].Location = new Point(buttons[2].Location.X - deslocamento, buttons[2].Location.Y + 40);
                            buttons[3].Location = new Point(buttons[3].Location.X - deslocamento, buttons[3].Location.Y + 60);
                            break;
                    }
                    rotation = rotation < 3 ? rotation + 1 : 0;
                    break;
                case 3:
                    switch (rotation)
                    {
                        case 0:
                            buttons[1].Location = new Point(buttons[1].Location.X - deslocamento, buttons[1].Location.Y + deslocamento);
                            buttons[2].Location = new Point(buttons[2].Location.X - 40, buttons[2].Location.Y + 40);
                            buttons[3].Location = new Point(buttons[3].Location.X - 60, buttons[3].Location.Y + 60);
                            break;
                        case 1:
                            buttons[1].Location = new Point(buttons[1].Location.X + deslocamento, buttons[1].Location.Y - deslocamento);
                            buttons[2].Location = new Point(buttons[2].Location.X + 40, buttons[2].Location.Y - 40);
                            buttons[3].Location = new Point(buttons[3].Location.X + 60, buttons[3].Location.Y - 60);
                            break;
                    }
                    rotation = rotation < 1 ? rotation + 1 : 0;
                    break;
                case 4:
                    rotation = 0;
                    break;
            }
        }

        private void move(string tecla)
        {
            bool moveL = true;
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Location.X == 0)
                {
                    moveL = false;
                    break;
                }
            }
            int xValue = 0;
            int yValue = 0;
            switch (tecla)
            {
                case "a":
                    if (moveL)
                    {
                        xValue = deslocamento;
                    }
                    break;
                case "d":
                    xValue = -deslocamento;
                    break;
                case "s":
                    yValue = -deslocamento;
                    break;
            }
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Location = new Point(buttons[i].Location.X - (xValue), buttons[i].Location.Y - (yValue));
            }
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
        }
    }
}
