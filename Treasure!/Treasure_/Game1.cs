using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Net;
namespace Treasure_
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Texture2D tileTexture;
        Texture2D headpieceTexture;
        Texture2D winTexture;
        SpriteFont font14;
        //DisplayMode displayMode;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle startGameButton = new Rectangle(500, 112, 50, 50);
        public THunter[] players;
        public static Random rnd = new Random();
        static public int height = 10;
        static public int width = 10;
        static public int tileHeight = 137;
        static public int tileWidth = 136;
        static public int swampSize = 3; 
        public Tile[,] tiles = new Tile[width, height];
        public Tile [] portals = new Tile[3];
        public Tile [] homes;
        public Vector2 treasurePos = new Vector2();
        public int wallSpawn = 20;
        GamePole gamepole = new GamePole();
        public Tile lastRiverTile = new Tile();
        public bool isMapGood = true;
        public List <Point> pp = new List<Point>();
        bool isPlayers = false;
        bool goPlayer = false;
        bool isWinner = false;
        int winner = 0;
        int playerNow = 0;
        int mouseX;
        int mouseY;
        int mouseLeftButton;
        bool drawStartGameButton = false;
        bool changePauseForUp = true;
        bool changePauseForDown = true;
        bool changePauseForLeft = true;
        bool changePauseForRight = true;
        bool changePauseFor8 = true;
        bool changePauseFor6 = true;
        bool changePauseFor4 = true;
        bool changePauseFor2 = true;
        bool goGame = false;
        int scrollWheelValue;
        int round = 1;
        string kill = " was killed";
        List <THunter> killedPlayers = new List<THunter>();
        bool pressedM = false;
        public Game1()
        {
            //displayMode.Height;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 780; //graphics.PreferredBackBufferHeight * 2;
            graphics.PreferredBackBufferWidth = 1280;// graphics.PreferredBackBufferWidth * 2;
            Content.RootDirectory = "Content";
            for (int i = 0;i < portals.GetLength(0); i++)
            {
                portals[i] = new Tile();
            }
            for (int x = 0;x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = new Tile() {type = 2};
                }
            }
            
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tileTexture = Content.Load<Texture2D>("TileTexture");
            font14 = Content.Load<SpriteFont>("font14");
            headpieceTexture = Content.Load<Texture2D>("headpiece");
            winTexture = Content.Load<Texture2D>("sokrovischa");
            IsMouseVisible = true;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            drawStartGameButton = false;
            var mouse = Mouse.GetState();
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.M))
            {
                pressedM = true;
            }
            else
            {
                pressedM = false;
            }
            scrollWheelValue = mouse.ScrollWheelValue;
            mouseLeftButton = (int)mouse.LeftButton;
            mouseX = mouse.X;
            mouseY = mouse.Y;
            if (startGameButton.Contains(mouseX, mouseY))
            {
                if (((int)mouse.LeftButton == 1))
                {
                    goGame = true;
                }
                else
                {
                    drawStartGameButton = true;
                }
            }
            if (goGame)
            {
                if (!isPlayers)
                {
                    if (keyboardState.IsKeyDown(Keys.D2))
                    {
                        players = new THunter[2];
                        homes = new Tile[2];
                        isPlayers = true;
                    }
                    if (keyboardState.IsKeyDown(Keys.D3))
                    {
                        players = new THunter[3];
                        homes = new Tile[3];
                        isPlayers = true;
                    }
                    if (keyboardState.IsKeyDown(Keys.D4))
                    {
                        players = new THunter[4];
                        homes = new Tile[4];
                        isPlayers = true;
                    }
                    if (keyboardState.IsKeyDown(Keys.D5))
                    {
                        players = new THunter[5];
                        homes = new Tile[5];
                        isPlayers = true;
                    }
                    if (keyboardState.IsKeyDown(Keys.D6))
                    {
                        players = new THunter[6];
                        homes = new Tile[6];
                        isPlayers = true;
                    }
                    if (isPlayers)
                    {
                        for (int i = 0; i < players.Length; i++)
                        {
                            homes[i] = new Tile();
                            players[i] = new THunter();
                            players[i].number = i;
                            players[i].killedPlayersInt.Add(0);
                            players[i].killedPlayersInt.Add(0);
                            players[i].killedPlayersInt.Add(0);
                        }
                        GamePole.Generate(this);
                    }
                }/*////////////////////////////////////*/
                else
                {
                    for (int i = 0; i < players.Count(); i++)
                    {
                        while ((players[i].ammunition != 3) && (players[i].homeAmmunition > 0))
                        {
                            players[i].ammunition++;
                            players[i].homeAmmunition--;
                        }
                    }
                    Vector2 vector = new Vector2(100, 100);
                    if (keyboardState.IsKeyDown(Keys.Up))
                    {
                        if (changePauseForUp)
                        {
                            changePauseForUp = false;
                            vector = new Vector2(0, -1);
                            players[playerNow].hodi.Add(" Up - ");
                            if (tiles[(int)players[playerNow].pPos.X, (int)players[playerNow].pPos.Y].up)
                            {
                                players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi[players[playerNow].hodi.Count - 1] + " Wall ";
                                goPlayer = true;
                                goto break2;
                            }
                            goto break1;
                        }
                    }
                    else
                        changePauseForUp = true;
                    if (keyboardState.IsKeyDown(Keys.Down))
                    {
                        if (changePauseForDown)
                        {
                            changePauseForDown = false;
                            vector = new Vector2(0, 1);
                            players[playerNow].hodi.Add(" Down  - ");
                            if (tiles[(int)players[playerNow].pPos.X, (int)players[playerNow].pPos.Y].down)
                            {
                                players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi[players[playerNow].hodi.Count - 1] + " Wall ";
                                goPlayer = true;
                                goto break2;
                            }
                            goto break1;
                        }
                    }
                    else
                        changePauseForDown = true;
                    if (keyboardState.IsKeyDown(Keys.Left))
                    {
                        if (changePauseForLeft)
                        {
                            changePauseForLeft = false;
                            vector = new Vector2(-1, 0);
                            players[playerNow].hodi.Add(" Left - ");
                            if (tiles[(int)players[playerNow].pPos.X, (int)players[playerNow].pPos.Y].left)
                            {
                                players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi[players[playerNow].hodi.Count - 1] + " Wall ";
                                goPlayer = true;
                                goto break2;
                            }
                            goto break1;
                        }
                    }
                    else
                        changePauseForLeft = true;
                    if (keyboardState.IsKeyDown(Keys.Right))
                    {
                        if (changePauseForRight)
                        {
                            changePauseForRight = false;
                            vector = new Vector2(1, 0);
                            players[playerNow].hodi.Add(" Right - ");
                            if (tiles[(int)players[playerNow].pPos.X, (int)players[playerNow].pPos.Y].right)
                            {
                                players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi[players[playerNow].hodi.Count - 1] + " Wall ";
                                goPlayer = true;
                                goto break2;
                            }
                            goto break1;
                        }
                    }
                    else
                        changePauseForRight = true;
                    break1:
                    ////////////////////////////////////////////
                    if (vector != new Vector2(100, 100))
                    {
                        switch (tiles[(int)players[playerNow].pPos.X + (int)vector.X, (int)players[playerNow].pPos.Y + (int)vector.Y].type)
                        {
                            case 0:
                                players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi[players[playerNow].hodi.Count - 1] + " Swamp ";
                                break;
                            case 1:
                                players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi[players[playerNow].hodi.Count - 1] + (" River");
                                players[playerNow].pPos += vector;
                                for (int i = 0; i < players.Length; i++)
                                {
                                    if ((i != playerNow) && (players[playerNow].pPos == players[i].pPos))
                                    {
                                        players[i].pPos = homes[i].position;
                                        players[i].killedPlayersInt[round + (int)((playerNow + players.Count() - i) / players.Count())]++;
                                        killedPlayers.Add(players[i]);
                                        if (players[i].haveTreasure)
                                        {
                                            players[i].haveTreasure = false;
                                            players[playerNow].haveTreasure = true;
                                            players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi.Last() + " with TREASURE! ";
                                        }
                                    }
                                }
                                if (players[playerNow].pPos == treasurePos)
                                {
                                    players[playerNow].haveTreasure = true;
                                    players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi.Last() + " with TREASURE! ";
                                    treasurePos = new Vector2(height, width);
                                }
                                if (tiles[(int)players[playerNow].pPos.X, (int)players[playerNow].pPos.Y].nextRiver == null)
                                {
                                    players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi[players[playerNow].hodi.Count - 1] + (" - Grate ");
                                }
                                else
                                {
                                    players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi[players[playerNow].hodi.Count - 1] + (" - River");
                                    for (int i = 0; i < players.Length; i++)
                                    {
                                        if ((i != playerNow) && (tiles[(int)players[playerNow].pPos.X, (int)players[playerNow].pPos.Y].nextRiver.position == players[i].pPos))
                                        {
                                            players[i].pPos = homes[i].position;
                                            players[i].killedPlayersInt[round + (int)((playerNow + players.Count() - i) / players.Count())]++;
                                            killedPlayers.Add(players[i]);
                                            if (players[i].haveTreasure)
                                            {
                                                players[i].haveTreasure = false;
                                                players[playerNow].haveTreasure = true;
                                                players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi.Last() + " with TREASURE! ";
                                            }
                                        }
                                    }
                                    if (tiles[(int)players[playerNow].pPos.X, (int)players[playerNow].pPos.Y].nextRiver.position == treasurePos)
                                    {
                                        players[playerNow].haveTreasure = true;
                                        players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi.Last() + " with TREASURE! ";
                                        treasurePos = new Vector2(height, width);
                                    }
                                    if (tiles[(int)players[playerNow].pPos.X, (int)players[playerNow].pPos.Y].nextRiver.nextRiver == null)
                                    {
                                        players[playerNow].pPos = tiles[(int)players[playerNow].pPos.X, (int)players[playerNow].pPos.Y].nextRiver.position;
                                        players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi[players[playerNow].hodi.Count - 1] + (" - Grate ");
                                    }
                                    else
                                    {
                                        players[playerNow].pPos = tiles[(int)players[playerNow].pPos.X, (int)players[playerNow].pPos.Y].nextRiver.nextRiver.position;
                                        players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi[players[playerNow].hodi.Count - 1] + (" - River ");
                                    }
                                }
                                break;
                            case 2:
                                players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi[players[playerNow].hodi.Count - 1] + ("Ground ");
                                players[playerNow].pPos += vector;
                                break;
                            case 3:
                                players[playerNow].pPos += vector;
                                int u = 0;
                                for (int i = 0; i < homes.Length; i++)
                                {
                                    if (homes[i].position == /*new Vector2(players[playerNow].pPos.X, players[playerNow].pPos.Y - 1)*/players[playerNow].pPos)
                                    {
                                        u = i;
                                        break;
                                    }
                                }
                                if (u == playerNow)
                                    players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi[players[playerNow].hodi.Count - 1] + ("Your home ");
                                else
                                    players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi[players[playerNow].hodi.Count - 1] + ("players" + u + "Home  ");
                                break;
                            case 4:
                                int k = 0;
                                for (int i = 0; i < portals.Length; i++)
                                {
                                    if (portals[i].position == new Vector2((int)players[playerNow].pPos.X + (int)vector.X, (int)players[playerNow].pPos.Y + (int)vector.Y))
                                    {
                                        k = i;
                                        break;
                                    }
                                }
                                players[playerNow].pPos += vector;
                                players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi[players[playerNow].hodi.Count - 1] + ("Portal " + (k + 1).ToString());
                                for (int i = 0; i < players.Length; i++)
                                {
                                    if ((i != playerNow) && (players[playerNow].pPos == players[i].pPos))
                                    {
                                        players[i].pPos = homes[i].position;
                                        players[i].killedPlayersInt[round + (int)((playerNow + players.Count() - i) / players.Count())]++;
                                        killedPlayers.Add(players[i]);
                                        if (players[i].haveTreasure)
                                        {
                                            players[i].haveTreasure = false;
                                            players[playerNow].haveTreasure = true;
                                            players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi.Last() + " with TREASURE! ";
                                        }
                                    }
                                }
                                if (players[playerNow].pPos == treasurePos)
                                {
                                    players[playerNow].haveTreasure = true;
                                    players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi.Last() + " with TREASURE! ";
                                    treasurePos = new Vector2(height, width);
                                }
                                players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi[players[playerNow].hodi.Count - 1] + " to portal " + ((k + 1) % portals.Length + 1).ToString();
                                players[playerNow].pPos = portals[(k + 1) % portals.Length].position;
                                break;
                        }
                        for (int i = 0; i < players.Length; i++)
                        {
                            if ((i != playerNow) && (players[playerNow].pPos == players[i].pPos))
                            {
                                players[i].pPos = homes[i].position;
                                players[i].killedPlayersInt[round + (int)((playerNow + players.Count() - i) / players.Count())]++;
                                killedPlayers.Add(players[i]);
                                if (players[i].haveTreasure)
                                {
                                    players[i].haveTreasure = false;
                                    players[playerNow].haveTreasure = true;
                                    players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi.Last() + " with TREASURE! ";
                                }
                            }
                        }
                        if (players[playerNow].pPos == treasurePos)
                        {
                            players[playerNow].haveTreasure = true;
                            players[playerNow].hodi[players[playerNow].hodi.Count - 1] = players[playerNow].hodi.Last() + " with TREASURE! ";
                            treasurePos = new Vector2(height, width);
                        }
                        goPlayer = true;
                        goto break2;
                    }
                    /////////////////////////////////////////////////////////////////////////////////////
                    if (players[playerNow].ammunition != 0)
                    {
                        Vector2 p = players[playerNow].pPos;
                        if (keyboardState.IsKeyDown(Keys.NumPad8))
                        {
                            if (changePauseFor8)
                            {
                                players[playerNow].hodi.Add("shot up");
                                changePauseFor8 = false;
                                goPlayer = true;
                                players[playerNow].ammunition--;
                                p.Y--;
                                while (p.Y >= 0)
                                {
                                    if (tiles[(int)p.X, (int)p.Y].down)
                                    {
                                        tiles[(int)p.X, (int)p.Y].down = false;
                                        tiles[(int)p.X, (int)p.Y + 1].up = false;
                                        goto break2;
                                    }
                                    for (int i = 0; i < players.Length; i++)
                                    {
                                        if (p == players[i].pPos)
                                        {
                                            if (players[i].haveTreasure)
                                            {
                                                players[i].haveTreasure = false;
                                                treasurePos = players[i].pPos;
                                            }
                                            players[i].pPos = homes[i].position;
                                            players[i].killedPlayersInt[round + (int)((playerNow + players.Count() - i) / players.Count())]++;
                                            killedPlayers.Add(players[i]);
                                            goto break2;
                                        }
                                    }
                                    p.Y--;
                                }
                                goto break2;
                            }
                        }
                        else
                            changePauseFor8 = true;
                        if (keyboardState.IsKeyDown(Keys.NumPad6))
                        {
                            if (changePauseFor6)
                            {
                                players[playerNow].hodi.Add("shot right");
                                changePauseFor6 = false;
                                goPlayer = true;
                                players[playerNow].ammunition--;
                                p.X++;
                                while (p.X < width)
                                {
                                    if (tiles[(int)p.X, (int)p.Y].left)
                                    {
                                        tiles[(int)p.X, (int)p.Y].left = false;
                                        tiles[(int)p.X - 1, (int)p.Y].right = false;
                                        goto break2;
                                    }
                                    for (int i = 0; i < players.Length; i++)
                                    {
                                        if (p == players[i].pPos)
                                        {
                                            if (players[i].haveTreasure)
                                            {
                                                players[i].haveTreasure = false;
                                                treasurePos = players[i].pPos;
                                            }
                                            players[i].pPos = homes[i].position;
                                            players[i].killedPlayersInt[round + (int)((playerNow + players.Count() - i) / players.Count())]++;
                                            killedPlayers.Add(players[i]);
                                            goto break2;
                                        }
                                    }
                                    p.X++;
                                }
                                goto break2;
                            }
                        }
                        else
                            changePauseFor6 = true;
                        if (keyboardState.IsKeyDown(Keys.NumPad4))
                        {
                            if (changePauseFor4)
                            {
                                players[playerNow].hodi.Add("shot left");
                                changePauseFor4 = false;
                                goPlayer = true;
                                players[playerNow].ammunition--;
                                p.X--;
                                while (p.X >= 0)
                                {
                                    if (tiles[(int)p.X, (int)p.Y].right)
                                    {
                                        tiles[(int)p.X, (int)p.Y].right = false;
                                        tiles[(int)p.X + 1, (int)p.Y].left = false;
                                        goto break2;
                                    }
                                    for (int i = 0; i < players.Length; i++)
                                    {
                                        if (p == players[i].pPos)
                                        {
                                            if (players[i].haveTreasure)
                                            {
                                                players[i].haveTreasure = false;
                                                treasurePos = players[i].pPos;
                                            }
                                            players[i].pPos = homes[i].position;
                                            players[i].killedPlayersInt[round + (int)((playerNow + players.Count() - i) / players.Count())]++;
                                            killedPlayers.Add(players[i]);
                                            goto break2;
                                        }
                                    }
                                    p.X--;
                                }
                                goto break2;
                            }
                        }
                        else
                            changePauseFor4 = true;
                        if (keyboardState.IsKeyDown(Keys.NumPad2))
                        {
                            if (changePauseFor2)
                            {
                                players[playerNow].hodi.Add("shot down");
                                changePauseFor2 = false;
                                goPlayer = true;
                                players[playerNow].ammunition--;
                                p.Y++;
                                while (p.Y < height)
                                {
                                    if (tiles[(int)p.X, (int)p.Y].up)
                                    {
                                        tiles[(int)p.X, (int)p.Y].up = false;
                                        tiles[(int)p.X, (int)p.Y - 1].down = false;
                                        goto break2;
                                    }
                                    for (int i = 0; i < players.Length; i++)
                                    {
                                        if (p == players[i].pPos)
                                        {
                                            if (players[i].haveTreasure)
                                            {
                                                players[i].haveTreasure = false;
                                                treasurePos = players[i].pPos;
                                            }
                                            players[i].pPos = homes[i].position;
                                            players[i].killedPlayersInt[round + (int)((playerNow + players.Count() - i) / players.Count())]++;
                                            killedPlayers.Add(players[i]);
                                            goto break2;
                                        }
                                    }
                                    p.Y++;
                                }
                                goto break2;
                            }
                        }
                        else
                            changePauseFor2 = true;
                    }
                    break2:
                    while (killedPlayers.Count != 0)
                    {
                        for (int i = 0; i < players.Length; i++)
                        {
                            if ((i != killedPlayers[0].number) && (players[i].pPos == homes[killedPlayers[0].number].position))
                            {
                                players[i].killedPlayersInt[round]++;
                                players[i].pPos = homes[i].position;
                                killedPlayers[0] = players[i];
                                goto Bb;
                            }
                        }
                        killedPlayers.RemoveAt(0);
                        Bb:;
                    }
                    for (int i = 0;i < players.Count(); i++)
                    {
                        if ((players[i].haveTreasure) && (players[i].pPos == homes[i].position))
                        {
                            isWinner = true;
                            winner = i + 1;
                        }
                    }
                    if (goPlayer)
                    {
                        playerNow = (playerNow + 1) % players.Length;
                        goPlayer = false;
                        if (playerNow == 0)
                        {
                            for (int i = 0; i < players.Count(); i++)
                            {
                                players[i].killedPlayersInt.Add(0);
                            }
                            round++;
                        }
                    }
                }
            }
                // Allows the  to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the  should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (!goGame)
            {
                spriteBatch.Draw(headpieceTexture, new Rectangle(-14, -4, 1290, 780), new Rectangle(0, 0, 1290, 795), Color.White);
                if (drawStartGameButton)
                {
                    spriteBatch.Draw(tileTexture, startGameButton, new Rectangle(240, 490, 90, 73), Color.White);
                }
            }
            else
            {
                if (isWinner)
                {
                    if (pressedM)
                    {
                        DrawPole();
                    }
                    else
                    {
                        spriteBatch.Draw(winTexture, new Rectangle(0, 0, 1277, 959), new Rectangle(0, 0, 1277, 959), Color.White);
                        spriteBatch.DrawString(font14, "player " + winner + " win!", new Vector2(graphics.PreferredBackBufferWidth / 2 - 84, graphics.PreferredBackBufferHeight / 2), new Color((255 - Color.Gold.R), (255 - Color.Gold.G), (255 - Color.Gold.B)));
                    }
                }
                else
                {
                    if (isPlayers)
                    {
                        //DrawPole();
                        GoGo();
                    }
                    else
                    {
                        WriteNumber();
                    }
                }
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        private void WriteNumber()
        {
            spriteBatch.DrawString(font14, "Please,enter number 2 - 6", new Vector2(graphics.PreferredBackBufferWidth / 2 - 175, graphics.PreferredBackBufferHeight / 2), Color.DarkSeaGreen);
        }
        private void DrawPole()
        {
            for (int w = 0;w < width; w++)
            {
                for (int h = 0;h < height; h++)
                {
                    spriteBatch.Draw(tileTexture, new Rectangle(w * 68, h * 68,68,68), new Rectangle(tileWidth * (tiles[w, h].type % 5),0, tileWidth, tileHeight), Color.White);
                }
            }
            spriteBatch.Draw(tileTexture, new Rectangle((int)treasurePos.X * 68, (int)treasurePos.Y * 68, 68, 68), new Rectangle(0, tileHeight, tileWidth, tileHeight), Color.White);
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    if (tiles[w, h].right)
                    {
                        spriteBatch.Draw(tileTexture, new Rectangle((w + 1) * 68 - 6, h * 68, 68, 68), new Rectangle(200, 266, tileWidth, tileHeight + 5), Color.White);
                    }
                    if (tiles[w, h].left)
                    {
                        spriteBatch.Draw(tileTexture, new Rectangle(w * 68 - 6, h * 68, 68, 68), new Rectangle(200, 266, tileWidth, tileHeight + 5), Color.White);
                    }
                    if (tiles[w, h].up)
                    {
                        spriteBatch.Draw(tileTexture, new Rectangle(w * 68 - 1, h * 68 - 3, 68, 68), new Rectangle(430, 330, tileWidth + 5, tileHeight), Color.White);
                    }
                    if ((tiles[w, h].type == 1) && (tiles[w, h].nextRiver == null))
                    {
                        spriteBatch.Draw(tileTexture, new Rectangle(68 * w - 2, (68 * (h + 1)) - 12, 68, 68), new Rectangle(425, 500, tileWidth + 10, tileHeight), Color.White);
                    }
                    else
                    {
                        if (tiles[w, h].down)
                        {
                            spriteBatch.Draw(tileTexture, new Rectangle(w * 68 - 1, (h + 1) * 68 - 3, 68, 68), new Rectangle(430, 330, tileWidth + 5, tileHeight), Color.White);
                        }
                    }
                    if ((tiles[w,h].number != 0)&&(tiles[w, h].type == 3))
                    {
                        spriteBatch.DrawString(font14, tiles[w, h].number.ToString(), new Vector2(w * 68 + 45, h * 68 + 45), Color.WhiteSmoke);
                    }
                    if ((tiles[w, h].number != 0) && (tiles[w, h].type == 4))
                    {
                        spriteBatch.DrawString(font14, tiles[w, h].number.ToString(), new Vector2(w * 68 + 45, h * 68 + 45), Color.Brown);
                    }
                }
            }
            for (int i = 0; i < players.Length; i++)
            {
                spriteBatch.Draw(tileTexture, new Rectangle((int)players[i].pPos.X * 68, (int)players[i].pPos.Y * 68, 68, 68), new Rectangle(14, 415, tileWidth, tileHeight), players[i].color);
            }
            spriteBatch.DrawString(font14, "now is the course of the player " + (playerNow + 1).ToString(), new Vector2(graphics.PreferredBackBufferWidth / 2 - 231, graphics.PreferredBackBufferHeight - 95), Color.DarkSeaGreen);
        }
        private void GoGo()
        {
            int lines = 0;
            for (int i = 0; true; i++)
            {
                for (int p = 0; p < players.Length; p++)
                {
                    if (players[p].hodi.Count != i)
                    {
                        spriteBatch.DrawString(font14, (p + 1).ToString() + ")  " + players[p].hodi[i], new Vector2(685, (font14.MeasureString("j").Y + 3) * lines + scrollWheelValue / 5), Color.DarkSeaGreen);
                        lines++;
                        switch (players[(p + 1) % players.Count()].killedPlayersInt[i + (int)((p + 1) / players.Count())])
                        {
                            case 0:
                                break;
                            case 1:
                                spriteBatch.DrawString(font14, "player " + (((p + 1) % players.Count()) + 1).ToString() + kill, new Vector2(685, (font14.MeasureString("j").Y + 3) * lines + scrollWheelValue / 5), Color.Red);
                                lines++;
                                break;
                            default:
                                spriteBatch.DrawString(font14, "player " + (((p + 1) % players.Count()) + 1).ToString() + kill + " by " + players[(p + 1) % players.Count()].killedPlayersInt[i + (int)((p + 1) / players.Count())] + " times", new Vector2(685, (font14.MeasureString("j").Y + 3) * lines + scrollWheelValue / 5), Color.Red);
                                lines++;
                                break;
                        }
                    }
                    else
                    {
                        goto Break;
                    }
                }
            }
            Break:;
        }
    }
}
