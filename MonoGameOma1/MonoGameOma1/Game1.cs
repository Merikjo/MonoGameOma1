using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGameOma1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        #region luokkamuuttujat

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        CollisionChecker cs;
        CollisionCheck cc;
        bool bCollision = false;

        Texture2D figa1; // tai mikä vain kuvan nimi
        Texture2D figa2; // tai mikä vain kuvan nimi
        Texture2D papla1;
        Texture2D papla2;
        Texture2D papla3;
        Texture2D papla4;
        Texture2D papla5;
        Texture2D figa1AnimA;
        Texture2D figa2Animaatio;
        Texture2D taustakuva;
        Texture2D piste;


        Texture2D sign; // tai mikä vain kuvan nimi
        Texture2D laatta; // tai mikä vain kuvan nimi

        Vector2 paikka; // sijainnin koordinaatit
        Vector2 paikka2;
        Vector2 papla1Paikka;


        int n = 8; // kokonaislukumuuttuja sijainnin x-koordinaatin muuttamiseksi
        int m = 1; // kokonaislukumuuttuja sijainnin y-koordinaatin muuttamiseksi
        int a = 2;
        int b = 1;

        int signy, signx;
        int signykoko;
        int signxkoko;

        int papla1y, papla1x;
        int papla1ykoko;
        int papla1xkoko;

        float paplaSpeed;


        int naytonLeveys;
        int naytonKorkeus;
        int alkupaikka;

        int intFiga1Nopeus = 1;
        int figa1Suunta = 1;
        int figa1Hidastaja;
        int figa1Hidastajanraja = 5;

        int figa2Hidastaja;
        int figa2Hidastajanraja = 5;

        BoundsCheck bc;

        private KeyboardState oldKeyboardState;
        private KeyboardState oldKeyboardState2;

        SpriteFont omaFontti;

        int x = 300;
        int y = 300;

        Figa1 flikka;
        Figa2 sankari;

        int figa1X = 100;
        int figa1Y = 100;

        int figa2X = 0; //spritesheet-animaation muuttuv koordinaatti

        //Non-Player Character AI
        bool eteenpain = true;
        bool eteenpain2 = true;
        bool peruutus = false;
        bool peruutus2 = false;
        bool liikkeella = true;
        bool liikkeella2 = true;

        bool collisionDetected;
        bool pixelCollision;

        Random rnd; //satunnaisluku

        //buttoniin liittyvät muuttujat
        Rectangle button;
        Rectangle mouseRect;
        bool boolNappiaPainettu = false;
        int paikka_x = 200;
        int paikka_y = 200;
        int leveys = 300;
        int korkeus = 100;

        private MouseState curMouseState;
        private MouseState lastMouseState;

        float xpoint = 0f;
        float ypoint = 0f;

        float rot = 0f; //asteina, ohjataan R ja E näppäimmillä
        private float fn;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //cs = new CollisionChecker(this);
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

            cs.Init();

            bc = new BoundsCheck();
            paikka = new Vector2(300f, 200f);

            flikka = new Figa1(this);
            //sankari = new Figa2();

            //signy = 300; signx = 100;
            //signykoko = 500;
            //signxkoko = 100;

            //papla1y = 300; papla1x = 100;
            //papla1ykoko = 500;
            //papla1xkoko = 100;
            //papla1Paikka = new Vector2(graphics.PreferredBackBufferWidth / 2,
            //graphics.PreferredBackBufferHeight / 2);
            //paplaSpeed = 100f;

            naytonLeveys = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            naytonKorkeus = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //aseta peli-ikkunalle koko tarvittaessa
            if (naytonLeveys >= 2500)
            {
                naytonLeveys = 2500;
            }
            if (naytonKorkeus >= 1000)
            {
                naytonKorkeus = 1000;
            }
            graphics.PreferredBackBufferWidth = naytonLeveys;
            graphics.PreferredBackBufferHeight = naytonKorkeus;
            graphics.ApplyChanges();

            rnd = new Random(); //satunnaisluvun luominen
            button = new Rectangle(paikka_x, paikka_y, leveys, korkeus); //buttonin määritykset

            Mouse.PlatformSetCursor(MouseCursor.Hand);
            IsMouseVisible = true;
            cc = new CollisionCheck();

            flikka.InitFiga1();

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

            // TODO: use this.Content to load your game content here
            figa1 = Content.Load<Texture2D>("figa1AnimaatioA");
            //figa2 = Content.Load<Texture2D>("Figa2");
            papla1 = Content.Load<Texture2D>("Figa1Papla1");
            papla2 = Content.Load<Texture2D>("Figa1Papla2");
            papla3 = Content.Load<Texture2D>("Figa1Papla3");
            papla4 = Content.Load<Texture2D>("Figa1Papla4");
            papla5 = Content.Load<Texture2D>("Figa1Papla5");
            //figa1AnimA = Content.Load<Texture2D>("figa1AnimaatioA");
            //figa2Animaatio = Content.Load<Texture2D>("#");

            //oman fontin lataus:
            omaFontti = Content.Load<SpriteFont>("Arial20");
            piste = Content.Load<Texture2D>("valkopiste");

            LoadGraphics();
        }

        public void LoadGraphics()
        {
            flikka.figa1 = this.figa1AnimA;
        }

        #region
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        #endregion

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //paikka.X += n;
            //if (paikka.X > 300) n = -n;
            //if (paikka.X < 100) n = -n;

            //käsittelee näppäimistön tilan 

            KeyboardState newKeyboardState = Keyboard.GetState();

            collisionDetected = false;

            bCollision = false;

            figa1Hidastaja++;

            if (figa1Hidastaja > figa1Hidastajanraja)
            {
                figa1Hidastaja = 0;
            }
            if (figa1Hidastaja == 0)
            {
                intFiga1Nopeus = rnd.Next(1, 4);
                int muutos = rnd.Next(1, 3);
                if (muutos == 1) figa1Suunta++;
                if (muutos == 2) figa1Suunta--;
                if (figa1Suunta > 8) figa1Suunta = 1;
                if (figa1Suunta < 1) figa1Suunta = 8;
            }
            if (figa1Suunta == 1)
            { //KOILLINEN
                int bcx = figa1X;
                int bcy = figa1Y;
                bcx += intFiga1Nopeus;
                bcy -= intFiga1Nopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    figa1X += intFiga1Nopeus;
                    figa1Y -= intFiga1Nopeus;
                }
            }
            if (figa1Suunta == 2)

            //if (ita)
            { //ITÄ
                int bcx = figa1X;
                int bcy = figa1Y;
                bcx += intFiga1Nopeus;
                //bcy -= intFiga1Nopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    figa1X += intFiga1Nopeus;
                    //figa1Y--;
                }
            }
            if (figa1Suunta == 3)
            //if (kaakko)
            { //KAAKKO
                int bcx = figa1X;
                int bcy = figa1Y;
                bcx += intFiga1Nopeus;
                bcy += intFiga1Nopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    figa1X += intFiga1Nopeus;
                    figa1Y += intFiga1Nopeus;
                }
            }
            if (figa1Suunta == 4)
            //if (etela)
            { //ETELÄ
                int bcx = figa1X;
                int bcy = figa1Y;
                //bcx += intFiga1Nopeus;
                bcy += intFiga1Nopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    //figa1X;
                    figa1Y += intFiga1Nopeus;
                }
            }

            if (figa1Suunta == 5)
            //if (lounas)
            { //LOUNAS
                int bcx = figa1X;
                int bcy = figa1Y;
                bcx -= intFiga1Nopeus;
                bcy += intFiga1Nopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    figa1X -= intFiga1Nopeus;
                    figa1Y += intFiga1Nopeus;
                }
            }
            if (figa1Suunta == 6)
            { //LÄNSI
                int bcx = figa1X;
                int bcy = figa1Y;
                bcx -= intFiga1Nopeus;
                //bcy -= intFiga1Nopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    figa1X -= intFiga1Nopeus;
                    //figa1Y--;
                }
            }
            if (figa1Suunta == 7)
            //if (luode)
            { //LUODE
                int bcx = figa1X;
                int bcy = figa1Y;
                bcx -= intFiga1Nopeus;
                bcy -= intFiga1Nopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    figa1X -= intFiga1Nopeus;
                    figa1Y -= intFiga1Nopeus;
                }
            }
            if (figa1Suunta == 8)
            //if (pohjoinen)
            { //POHJOINEN
                int bcx = figa1X;
                int bcy = figa1Y;
                //bcx += intFiga1Nopeus;
                bcy -= intFiga1Nopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    //figa1X++;
                    figa1Y -= intFiga1Nopeus;
                }
            }
            if (cc.Check(figa1X, figa1Y, (int)paikka.X, (int)paikka.Y))
            {
                Console.WriteLine("Törmäys havaittu");
                bCollision = true;
            }
            flikka.Update(gameTime);
            Rectangle r = new Rectangle();

            if (r.Width > 1 || r.Height > 1)

            {
                collisionDetected = true;
                pixelCollision = cs.Check(GraphicsDevice, figa2, figa1AnimA, sankari.paikka, flikka.paikka, sankari.rect);
                //Console.WriteLine("r.Width = " + r.Width);
                //Console.WriteLine("r.Height = " + r.Height);
            }



            ////Figa1;
            //figa1Hidastaja++;
            //if (!peruutus)
            //{
            //    if (figa1Hidastaja > figa1Hidastajanraja)
            //    {
            //        //paikka2.Y += b;
            //        //if (paikka2.Y > 200) b = -b;
            //        //if (paikka2.Y < 0) b = -b;
            //        //figa2hidastaja = 0;

            //        figa1X -= 80;
            //        if (figa1X < 80) figa1X = 320;
            //        figa1Hidastaja = 0;
            //    }
            //}
            //else
            //{
            //    if (figa1Hidastaja > figa1Hidastajanraja)
            //    {
            //        figa1X += 80;
            //        if (figa1X > 320) figa1X = 80;
            //        figa1Hidastaja = 0;
            //    }
            //}


            //Figa2:
            figa2Hidastaja++;
            if (!peruutus2)
            {
                if (figa2Hidastaja > figa2Hidastajanraja)
                {
                    //paikka2.Y += b;
                    //if (paikka2.Y > 200) b = -b;
                    //if (paikka2.Y < 0) b = -b;
                    //figa2hidastaja = 0;

                    figa2X -= 80;
                    if (figa2X < 80) figa2X = 320;
                    figa2Hidastaja = 0;
                }
            }
            else
            {
                if (figa2Hidastaja > figa2Hidastajanraja)
                {
                    figa2X += 80;
                    if (figa2X > 320) figa2X = 80;
                    figa2Hidastaja = 0;
                }
            }

            boolNappiaPainettu = false;
            curMouseState = Mouse.GetState();
            //käsittelee näppäimistön tilan
            if (curMouseState.LeftButton == ButtonState.Pressed)

            {
                Point mousePositionPoint = curMouseState.Position;
                mouseRect = new Rectangle(mousePositionPoint, new Point(300, 100));
                if (mouseRect.Intersects(button))
                {
                    boolNappiaPainettu = true;
                    Console.WriteLine("Nappia painettu");
                }
            }

            KeyboardState newKeyBoardState = Keyboard.GetState();
            if (newKeyBoardState.IsKeyDown(Keys.Left))
            {
                //liiku vasemmale
                liikkeella = true;
                if (!peruutus) { paikka.X -= a; eteenpain = true; }
                if (peruutus) { paikka.X -= a; eteenpain = false; }
                //paikka.X -= a;
                //eteenpain = false;
            }
            else liikkeella = false;

            if (newKeyboardState.IsKeyDown(Keys.Down))
            {
                //hahmo.Liiku ylös
                liikkeella = true;
                if (!peruutus) { paikka.Y += n; eteenpain = true; }
                if (peruutus) { paikka.Y += n; eteenpain = false; }
            }
            else
                liikkeella = false;

            if (newKeyBoardState.IsKeyDown(Keys.Right))
            {
                //liiku oikealle
                liikkeella = true;
                if (!peruutus) { paikka.X += a; eteenpain = true; }
                if (peruutus) { paikka.X += a; eteenpain = false; }
                //paikka.X -= a;
                //eteenpain = false;
            }
            else liikkeella = false;

            if (newKeyBoardState.IsKeyDown(Keys.B))
            {
                peruutus = true;
            }

            if (newKeyBoardState.IsKeyDown(Keys.F))
            {
                peruutus = false;
            }

            //rotaationäppäimet

            if (newKeyboardState.IsKeyDown(Keys.X))
            {
                xpoint += 10f;
            }

            if (newKeyboardState.IsKeyDown(Keys.Z))
            {
                xpoint -= 10f;
            }

            if (newKeyboardState.IsKeyDown(Keys.Y))
            {
                ypoint += 10f;
            }

            if (newKeyboardState.IsKeyDown(Keys.T))
            {
                xpoint -= 10f;
            }

            if (newKeyboardState.IsKeyDown(Keys.R))
            {
                rot -= 30f; //degrees
            }

            if (newKeyboardState.IsKeyDown(Keys.E))
            {
                rot += 30f; //degrees
            }

            oldKeyboardState = newKeyBoardState; //tallenna vanha tila, jos tarpeen

            int kortti = rnd.Next(52);  // tilapäismuuttuja kortti saa arvon välillä 0 - 51

            //KeyboardState newKeyBoardState2 = Keyboard.GetState();
            ////käsittelee näppäimistön tilan
            //if (newKeyBoardState2.IsKeyDown(Keys.Left))
            //{
            //    //liiku vasemmale
            //    if (!peruutus2) { paikka2.X -= b; eteenpain2 = true; }
            //    //paikka.X -= a;
            //    //eteenpain = false;
            //}
            //else liikkeella2 = false;

            //if (newKeyBoardState2.IsKeyDown(Keys.Right))
            //{
            //    //liiku oikealle
            //    if (!peruutus2) { paikka2.X += b; eteenpain2 = true; }
            //    if (peruutus2) { paikka2.X += b; eteenpain2 = false; }
            //    //paikka.X -= a;
            //    //eteenpain = false;
            //}
            //else liikkeella2 = false;

            //if (newKeyBoardState2.IsKeyDown(Keys.Q))
            //{
            //    peruutus2 = true;
            //}

            //if (newKeyBoardState2.IsKeyDown(Keys.R))
            //{
            //    peruutus2 = false;
            //}


            //oldKeyboardState2 = newKeyBoardState2; //tallenna vanha tila, jos tarpeen
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //taustan väri
            GraphicsDevice.Clear(Color.LightPink);

            // TODO: Add your drawing code here

            // Draw background
            spriteBatch.Begin();
            spriteBatch.Draw(taustakuva, new Rectangle(0, 0, 1200, 800), Color.White);
            spriteBatch.End();

            //Draw Hahmot
            //sankaritar.Draw(gameTime);
            //sankari.Draw(gameTime);

            // Draw texts
            spriteBatch.Begin();
            Color textColor = new Color(Color.OrangeRed, 1f);

            string viesti0 = "Pelasta Figan Bad Hair päivä!";
            Vector2 alkupaikka0 = omaFontti.MeasureString(viesti0);
            spriteBatch.DrawString(omaFontti, viesti0, new Vector2((naytonLeveys - alkupaikka0.X - 300) / 2, naytonKorkeus / 2 - 400), Color.AliceBlue, 0f, new Vector2(0, 0), 3f, SpriteEffects.None, 0f);

            string viesti = "Hei, olen Figa!";
            Vector2 alkupaikka = omaFontti.MeasureString(viesti);
            spriteBatch.DrawString(omaFontti, viesti, new Vector2((naytonLeveys - alkupaikka.X) / 2,
            naytonKorkeus / 2), Color.White);

            // Kokeillut värit: Gold, Fuchsia...
            //spriteBatch.Draw(pallo, paikka, Color.Gold);
            //spriteBatch.Draw(figa1, paikka, Color.White);
            //spriteBatch.Draw(figa2, paikka2, Color.White);
            //spriteBatch.Draw(figa1Animaatio, paikka2, Color.White);
            //spriteBatch.Draw(figa2Animaatio, paikka2, Color.White);

    
            string viesti1 = "Liikkuminen sivulle: nuolet vasemmalle ja oikealle";
            Vector2 alkupaikka1 = omaFontti.MeasureString(viesti1);
            spriteBatch.DrawString(omaFontti, viesti1, new Vector2((naytonLeveys - alkupaikka1.X) / 2, naytonKorkeus / 2 - 300), textColor); //tekstin tulostus

            string viesti2 = "Suunnanmuutos: B ja F, nopeus M ja L";
            Vector2 alkupaikka2 = omaFontti.MeasureString(viesti2);
            spriteBatch.DrawString(omaFontti, viesti2, new Vector2((naytonLeveys - alkupaikka2.X) / 2, naytonKorkeus / 2 - 300 + 40), textColor); //tekstin tulostus

            string viesti3 = "Lahemmaksi ja kauemmaksi: Yla- ja alanuoli. Rotaatiot: E, R, T, Z, X";
            Vector2 alkupaikka3 = omaFontti.MeasureString(viesti3);
            spriteBatch.DrawString(omaFontti, viesti3, new Vector2((naytonLeveys - alkupaikka3.X) / 2, naytonKorkeus / 2 - 300 + 80), textColor); //tekstin tulostus

            string viesti4 = "LOPETUS = ESC       ";

            Vector2 alkupaikka4 = new Vector2(20f, 20f);
            if (!collisionDetected)
            {
                spriteBatch.DrawString(omaFontti, viesti4, alkupaikka4, textColor); //tekstin tulostus
            }
            if (collisionDetected)
            {
                ///*
                if (collisionDetected) viesti4 = "TORMAYS HAVAITTU!";
                if (pixelCollision)
                {
                    viesti4 = "PIKSELITORMAYS!";
                    spriteBatch.Draw(cs.uusi, new Vector2(400f, 400f), Color.White);
                    spriteBatch.Draw(cs.uusiGhost, new Vector2(400f, 200f), Color.White);
                }

                alkupaikka4 = new Vector2(20f, 20f);
                spriteBatch.DrawString(omaFontti, viesti4, alkupaikka4, textColor); //tekstin tulostus
                flikka.viestilaskuri--;
                if (flikka.viestilaskuri < 0)
                {
                    flikka.viestilaskuri = 30; collisionDetected = false;
                    pixelCollision = false;
                }
            }

            //spriteBatch.Draw(piste, sankari.rect, Color.Red);


            if (liikkeella)
            {
                if (eteenpain)
                    spriteBatch.Draw(figa2Animaatio, paikka, new Rectangle(figa2X - 153, 0, 153, 227), Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.FlipHorizontally, 0f);

                if (!eteenpain)
                    spriteBatch.Draw(figa2Animaatio, paikka, new Rectangle(figa2X - 153, 0, 153, 227), Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
            }
            else
                spriteBatch.Draw(figa2Animaatio, paikka, new Rectangle(0, 0, 153, 227), Color.White);

            spriteBatch.Draw(figa2Animaatio, paikka, new Rectangle(0, 130, 1, 1), Color.White);

            if (boolNappiaPainettu)
            {
                spriteBatch.Draw(figa2Animaatio, button, new Rectangle(0, 130, 1, 100), Color.Red);
            }

            spriteBatch.Draw(figa1, new Vector2((float)figa1X, (float)figa1Y), new Rectangle(0, 0, 80, 120), Color.White);

            if (bCollision)

                spriteBatch.Draw(figa1, new Vector2((float)figa1X, (float)figa1Y), new Rectangle(0, 0, 80, 120), Color.Red);

            spriteBatch.Draw(papla1, papla1Paikka, null, Color.White, 0f, new Vector2(papla1.Width / 2, papla1.Height / 2), Vector2.One, SpriteEffects.None, 0f);

            //if (liikkeella2)
            //{
            //    if (eteenpain2)
            //        spriteBatch.Draw(figa2Animaatio, paikka2, new Rectangle(figa2X - 80, 0, 80, 120), Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.FlipHorizontally, 0f);

            //    if (!eteenpain2)
            //        spriteBatch.Draw(figa2Animaatio, paikka2, new Rectangle(figa2X - 80, 0, 80, 120), Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
            //}
            //else
            //    spriteBatch.Draw(figa2Animaatio, paikka2, new Rectangle(0, 0, 80, 120), Color.White);


            //spriteBatch.Draw(papla1, new Rectangle(300, 300, 1, 1), new Rectangle(350, 350, 2, 2), Color.White);
            //spriteBatch.Draw(papla1, new Vector2(x,y), Color.White); //ei suositeltava tapa
            //spriteBatch.Draw(laatta, new Vector2(300, 300), Color.Gold); //ei suositeltava tapa

            //spriteBatch.Draw(sign, new Rectangle(signx, signy, signxkoko, signykoko), Color.AntiqueWhite););
            //spriteBatch.Draw(pallo, new Rectangle(0, 0, 600, 400), new Rectangle(0, 0, 64, 36), Color.White);
            //spriteBatch.Draw(pallo, new Vector2(0f, 0f), new Rectangle(0, 0, 100, 100), Color.White, 0.3f, new Vector2(0,0), 0.0f, null, 1f);
            //spriteBatch.Draw(pallo, new Vector2(0f, 0f), new Rectangle(0,0,100,100), Color.Black, 0.0f, new Vector2(1,1), 1.0f, SpriteEffects.None, 0f);
            //spriteBatch.Draw()

   

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}