// Importar as bibliotecas necessárias
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace SpaceShooter // namespace do projeto
{
    public partial class Form1 : Form // classe do formulário que herda de Form
    {
        WindowsMediaPlayer gameMedia; // variável para tocar a música de fundo
        WindowsMediaPlayer shootgMedia; // variável para tocar o som do tiro
        WindowsMediaPlayer explosion; // variável para tocar o som da explosão

        PictureBox[] stars; // vetor de PictureBox para as estrelas
        PictureBox[] munitions; // vetor de PictureBox para as munições
        PictureBox[] enemies; // vetor de PictureBox para os inimigos
        PictureBox[] enemiesMunition; // vetor de PictureBox para as munições dos inimigos

        int backgroundspeed; // variável para controlar a velocidade do fundo
        int playerSpeed; // variável para controlar a velocidade do jogador
        int munitionSpeed; // variável para controlar a velocidade das munições
        int enemySpeed; // variável para controlar a velocidade dos inimigos
        int enemiesMunitionSpeed; // variável para controlar a velocidade das munições dos inimigos
        int score; // variável para controlar a pontuação
        int level; // variável para controlar o nível
        int difficulty; // variável para controlar a dificuldade
        bool pause; // variável para controlar o pause
        bool gameIsOver; // variável para controlar o fim do jogo

        Random rnd; // variável para gerar números aleatórios

        public Form1() // construtor da classe
        {
            InitializeComponent(); // inicializa os componentes do formulário
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Image munition = Image.FromFile(@"C:\Users\diogo\Documents\SpaceShooter\assets\munition.png");
            Image enemy1 = Image.FromFile(@"C:\Users\diogo\Documents\SpaceShooter\assets\E1.png");
            Image enemy2 = Image.FromFile(@"C:\Users\diogo\Documents\SpaceShooter\assets\E2.png");
            Image enemy3 = Image.FromFile(@"C:\Users\diogo\Documents\SpaceShooter\assets\E3.png");
            Image boss1 = Image.FromFile(@"C: \Users\diogo\Documents\SpaceShooter\assets\Boss1.png");
            Image boss2 = Image.FromFile(@"C:\Users\diogo\Documents\SpaceShooter\assets\Boss2.png");

            backgroundspeed = 4; // velocidade do fundo
            playerSpeed = 4; // velocidade do jogador
            enemySpeed = 4; // velocidade dos inimigos
            munitionSpeed = 20; // velocidade das munições
            enemiesMunitionSpeed = 4; // velocidade das munições dos inimigos
            pause = false; // pause
            gameIsOver = false; // fim do jogo
            score = 0; // pontuação
            level = 1; // nível
            difficulty = 9; // dificuldade

            enemies = new PictureBox[10]; // vetor de PictureBox para os inimigos
            munitions = new PictureBox[3]; // vetor de PictureBox para as munições
            stars = new PictureBox[15]; // vetor de PictureBox para as estrelas
            enemiesMunition = new PictureBox[10]; // vetor de PictureBox para as munições dos inimigos

            rnd = new Random(); // variável para gerar números aleatórios

            gameMedia = new WindowsMediaPlayer(); // variável para tocar a música de fundo
            shootgMedia = new WindowsMediaPlayer(); // variável para tocar o som do tiro
            explosion = new WindowsMediaPlayer(); // variável para tocar o som da explosão

            for (int i = 0; i < stars.Length; i++) // loop para criar as estrelas
            {
                stars[i] = new PictureBox(); // criar uma PictureBox
                stars[i].BorderStyle = BorderStyle.None; // remover borda
                stars[i].Location = new Point(rnd.Next(20, 580), rnd.Next(-10, 400)); // posição aleatória

                if (i % 2 == 1) // se o resto da divisão por 2 for igual a 1
                {
                    stars[i].Size = new Size(2, 2); // tamanho da PictureBox
                    stars[i].BackColor = Color.Wheat; // cor da PictureBox
                }
                else // caso contrário
                {
                    stars[i].Size = new Size(3, 3); // tamanho da PictureBox
                    stars[i].BackColor = Color.DarkGray; // cor da PictureBox
                }

                this.Controls.Add(stars[i]); // adicionar a PictureBox ao formulário
            }

            for (int i = 0; i < munitions.Length; i++) // loop para criar as munições
            {
                munitions[i] = new PictureBox(); // criar uma PictureBox
                munitions[i].Size = new Size(8, 8); // tamanho da PictureBox
                munitions[i].Image = munition; // imagem da PictureBox
                munitions[i].SizeMode = PictureBoxSizeMode.Zoom; // ajustar a imagem
                munitions[i].BorderStyle = BorderStyle.None; // remover borda
                this.Controls.Add(munitions[i]); // adicionar a PictureBox ao formulário
            }

            for (int i = 0; i < enemies.Length; i++) // loop para criar os inimigos
            {
                enemies[i] = new PictureBox(); // criar uma PictureBox
                enemies[i].Size = new Size(40, 40); // tamanho da PictureBox
                enemies[i].SizeMode = PictureBoxSizeMode.Zoom; // ajustar a imagem
                enemies[i].BorderStyle = BorderStyle.None; // remover borda
                enemies[i].Visible = false; // tornar a PictureBox invisível
                this.Controls.Add(enemies[i]); // adicionar a PictureBox ao formulário
                enemies[i].Location = new Point((i + 1) * 50, -50); // posição inicial
            }

            // imagens dos inimigos
            enemies[0].Image = boss1;
            enemies[1].Image = enemy2;
            enemies[2].Image = enemy3;
            enemies[3].Image = enemy3;
            enemies[4].Image = enemy1;
            enemies[5].Image = enemy3;
            enemies[6].Image = enemy2;
            enemies[7].Image = enemy3;
            enemies[8].Image = enemy2;
            enemies[9].Image = boss2;

            for(int i = 0; i < enemiesMunition.Length; i++) // loop para criar as munições dos inimigos
            {
                enemiesMunition[i] = new PictureBox(); // criar uma PictureBox
                enemiesMunition[i].Size = new Size(2, 25); // tamanho da PictureBox
                enemiesMunition[i].Visible = false; // tornar a PictureBox invisível
                enemiesMunition[i].BackColor = Color.Yellow; // cor da PictureBox

                int x = rnd.Next(0, 10); // gerar um número aleatório entre 0 e 9
                enemiesMunition[i].Location = new Point(enemies[x].Location.X, enemies[x].Location.Y - 20); // posição inicial
                this.Controls.Add(enemiesMunition[i]); // adicionar a PictureBox ao formulário
            }

            gameMedia.controls.play(); // tocar a música de fundo

            gameMedia.URL = @"C:\Users\diogo\Documents\SpaceShooter\songs\GameSong.mp3"; // caminho da música de fundo
            shootgMedia.URL = @"C:\Users\diogo\Documents\SpaceShooter\songs\shoot.mp3"; // caminho do som do tiro
            explosion.URL = @"C:\Users\diogo\Documents\SpaceShooter\songs\boom.mp3"; // caminho do som da explosão
            gameMedia.settings.setMode("loop", true); // repetir a música de fundo
            gameMedia.settings.volume = 5; // volume da música de fundo
            shootgMedia.settings.volume = 1; // volume do som do tiro
            explosion.settings.volume = 6; // volume do som da explosão
        }

        private void MoveBgTimer_Tick(object sender, EventArgs e) // evento do timer
        {
            for(int i = 0; i < stars.Length / 2; i++) // loop para mover as estrelas
            {
                stars[i].Top += backgroundspeed; // mover a PictureBox para baixo

                if(stars[i].Top >= this.Height) // se a PictureBox sair do formulário
                {
                    stars[i].Top = -stars[i].Height; // reposicionar a PictureBox
                }
            }

            for(int i = stars.Length / 2; i < stars.Length; i++) // loop para mover as estrelas
            {
                stars[i].Top += backgroundspeed - 2; // mover a PictureBox para baixo

                if(stars[i].Top >= this.Height) // se a PictureBox sair do formulário
                {
                    stars[i].Top = -stars[i].Height; // reposicionar a PictureBox
                }
            }
        }

        private void LeftMoveTimer_Tick(object sender, EventArgs e) // evento do timer
        {
            if(Player.Left > 10) Player.Left -= playerSpeed; // mover a PictureBox para a esquerda
        }

        private void RighMoveTimer_Tick(object sender, EventArgs e) // evento do timer
        {
            if (Player.Right < 580) Player.Left += playerSpeed; // mover a PictureBox para a direita
        }

        private void DownMoveTimer_Tick(object sender, EventArgs e) // evento do timer
        {
            if (Player.Top < 400) Player.Top += playerSpeed; // mover a PictureBox para baixo
        }

        private void UpMoveTimer_Tick(object sender, EventArgs e) // evento do timer
        {
            if (Player.Top > 10) Player.Top -= playerSpeed; // mover a PictureBox para cima
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) // evento do teclado
        {
            if(!pause) // se o jogo não estiver pausado
            {
                if (e.KeyCode == Keys.Right) // se a tecla pressionada for a seta para a direita
                {
                    RighMoveTimer.Start(); // iniciar o timer
                }

                if (e.KeyCode == Keys.Left) // se a tecla pressionada for a seta para a esquerda
                {
                    LeftMoveTimer.Start(); // iniciar o timer
                }

                if (e.KeyCode == Keys.Down) // se a tecla pressionada for a seta para baixo
                {
                    DownMoveTimer.Start(); // iniciar o timer
                }

                if (e.KeyCode == Keys.Up) // se a tecla pressionada for a seta para cima
                {
                    UpMoveTimer.Start(); // iniciar o timer
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) // evento do teclado quando a tecla é solta
        {
            RighMoveTimer.Stop(); // parar o timer
            LeftMoveTimer.Stop(); // parar o timer
            DownMoveTimer.Stop(); // parar o timer
            UpMoveTimer.Stop(); // parar o timer

            if(e.KeyCode == Keys.Space) // se a tecla pressionada for a barra de espaço
            {
                if(!gameIsOver) // se o jogo não tiver acabado
                {
                    if(pause) // se o jogo estiver pausado
                    {
                        StartTimers(); // iniciar os timers
                        label.Visible = false; // tornar a label invisível
                        gameMedia.controls.play(); // tocar a música de fundo
                        pause = false; // definir a variável pause como false
                    }
                    else // se o jogo não estiver pausado
                    {
                        label.Location = new Point(this.Width / 2 - 120, 150); // reposicionar a label
                        label.Text = "Paused!"; // definir o texto da label
                        label.Visible = true; // tornar a label visível
                        gameMedia.controls.pause(); // pausar a música de fundo
                        StopTimers(); // parar os timers
                        pause = true; // definir a variável pause como true
                    }
                }
            }
        }

        private void MoveMunitionTimer_Tick(object sender, EventArgs e) // evento do timer
        {
            for(int i = 0; i < munitions.Length; i++) // loop para mover as munições
            {
                if(munitions[i].Top > 0) // se a PictureBox não sair do formulário
                {
                    munitions[i].Visible = true; // tornar a PictureBox visível
                    munitions[i].Top -= munitionSpeed; // mover a PictureBox para cima
                    Collision(); // chamar a função de colisão
                }
                else
                {
                    munitions[i].Visible = false; // tornar a PictureBox invisível
                    munitions[i].Location = new Point(Player.Location.X + 20, Player.Location.Y - i * 30); // reposicionar a PictureBox
                }
            }
        }

        private void MoveEnemiesTimer_Tick(object sender, EventArgs e) // evento do timer
        {
            MoveEnemies(enemies, enemySpeed); // chamar a função de mover os inimigos
        }

        private void MoveEnemies(PictureBox[] array, int speed) // função para mover os inimigos
        {
            for(int i = 0; i < array.Length; i++) // loop para mover os inimigos
            {
                array[i].Visible = true; // tornar a PictureBox visível
                array[i].Top += speed; // mover a PictureBox para baixo

                if(array[i].Top > this.Height) // se a PictureBox sair do formulário
                {
                    array[i].Location = new Point((i + 1) * 50, -200); // reposicionar a PictureBox
                }
            }
        }

        private void Collision() // função para detetar colisões
        {
            for (int i = 0; i < enemies.Length; i++) // loop para detetar colisões
            {
                if(munitions[0].Bounds.IntersectsWith(enemies[i].Bounds) // se a PictureBox da munição colidir com a PictureBox do inimigo
                    || munitions[1].Bounds.IntersectsWith(enemies[i].Bounds) // se a PictureBox da munição colidir com a PictureBox do inimigo
                    || munitions[2].Bounds.IntersectsWith(enemies[i].Bounds)) // se a PictureBox da munição colidir com a PictureBox do inimigo
                {
                    explosion.controls.play(); // tocar o som de explosão

                    score += 1;
                    scorelbl.Text = (score < 10) ? 0 + score.ToString() : score.ToString(); // definir o texto da label

                    if(score % 30 == 0) // se o score for divisível por 30
                    {
                        level += 1;
                        levellbl.Text = (level < 10) ? 0 + level.ToString() : level.ToString(); // definir o texto da label

                        if(enemySpeed <= 10 && enemiesMunitionSpeed <= 10 && difficulty >= 0) // se a velocidade dos inimigos for menor ou igual a 10 e a velocidade das munições dos inimigos for menor ou igual a 10 e a dificuldade for maior ou igual a 0
                        {
                            difficulty--; // diminuir a dificuldade
                            enemySpeed++; // aumentar a velocidade dos inimigos
                            enemiesMunitionSpeed++; // aumentar a velocidade das munições dos inimigos
                        }

                        if(level == 10) // se o nível for igual a 10
                        {
                            GameOver("Nice Done!"); // chamar a função de Game Over
                        }
                    }

                    enemies[i].Location = new Point((i + 1) * 50, -100); // reposicionar a PictureBox do inimigo
                }

                if (Player.Bounds.IntersectsWith(enemies[i].Bounds)) // se a PictureBox do jogador colidir com a PictureBox do inimigo
                {
                    explosion.settings.volume = 30; // definir o volume do som de explosão
                    explosion.controls.play(); // tocar o som de explosão
                    Player.Visible = false; // tornar a PictureBox do jogador invisível
                    GameOver("Game Over!"); // chamar a função de Game Over
                }
            }
        }

        private void GameOver(string str) // função de Game Over
        {
            label.Text = str; // definir o texto da label
            label.Location = new Point(120, 120); // reposicionar a label
            label.Visible = true; // tornar a label visível
            ReplayBtn.Visible = true; // tornar o botão de replay visível
            ExitBtn.Visible = true; // tornar o botão de sair visível

            gameMedia.controls.stop(); // parar a música de fundo
            StopTimers(); // chamar a função de parar os timers
        }

        private void StopTimers() // função de parar os timers
        {
            MoveBgTimer.Stop(); // parar o timer de mover o fundo
            MoveEnemiesTimer.Stop(); // parar o timer de mover os inimigos
            MoveMunitionTimer.Stop(); // parar o timer de mover as munições
            EnemiesMunitionTimer.Stop(); // parar o timer de mover as munições dos inimigos
        }

        private void StartTimers() // função de iniciar os timers
        {
            MoveBgTimer.Start(); // iniciar o timer de mover o fundo
            MoveEnemiesTimer.Start(); // iniciar o timer de mover os inimigos
            MoveMunitionTimer.Start(); // iniciar o timer de mover as munições
            EnemiesMunitionTimer.Start(); // iniciar o timer de mover as munições dos inimigos
        }

        private void EnemiesMunitionTimer_Tick(object sender, EventArgs e) // evento do timer
        {
            for(int i = 0; i < (enemiesMunition.Length - difficulty); i++) // loop para mover as munições dos inimigos
            {
                if(enemiesMunition[i].Top < this.Height) // se a PictureBox não sair do formulário
                {
                    enemiesMunition[i].Visible = true; // tornar a PictureBox visível
                    enemiesMunition[i].Top += enemiesMunitionSpeed; // mover a PictureBox para baixo
                    CollisionWithEnemiesMunition(); // chamar a função de detetar colisões com as munições dos inimigos
                }
                else // se a PictureBox sair do formulário
                {
                    enemiesMunition[i].Visible = false; // tornar a PictureBox invisível

                    int x = rnd.Next(0, 10); // gerar um número aleatório entre 0 e 10
                    enemiesMunition[i].Location = new Point(enemies[x].Location.X + 20, enemies[x].Location.Y + 30); // reposicionar a PictureBox
                }
            }
        }

        private void CollisionWithEnemiesMunition() // função para detetar colisões com as munições dos inimigos
        {
            for (int i = 0; i < enemiesMunition.Length; i++) // loop para detetar colisões
            {
                if(enemiesMunition[i].Bounds.IntersectsWith(Player.Bounds)) // se a PictureBox da munição do inimigo colidir com a PictureBox do jogador
                {
                    enemiesMunition[i].Visible = false; // tornar a PictureBox invisível
                    explosion.settings.volume = 30; // definir o volume do som de explosão
                    explosion.controls.play(); // tocar o som de explosão
                    Player.Visible = false; // tornar a PictureBox do jogador invisível
                    GameOver("Game Over!"); // chamar a função de Game Over
                }
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e) // evento do botão de sair
        {
            Environment.Exit(1); // fechar o programa
        }

        private void ReplayBtn_Click(object sender, EventArgs e) // evento do botão de replay
        {
            this.Controls.Clear(); // limpar os controles do formulário
            InitializeComponent(); // inicializar os componentes do formulário
            Form1_Load(e, e); // chamar o evento do formulário
        }
    }
}
