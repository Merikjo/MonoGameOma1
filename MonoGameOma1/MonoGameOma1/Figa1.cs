using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace MonoGameOma1
{
    class Figa1 : Hahmo
    {
        Miekka miekka; //bool? Miekka-luokan ilmentymä?, int = miekan kunto?
        private KeyboardState oldKeyboardState;
        public SpriteBatch spriteBatch;

        public Texture2D figa1; //spritesheet, 4 kuvaa a 80x120

        public Vector2 paikka; // sijainnin koordinaatit

        int figa2X = 0; //spritesheet-animaation muuttuv koordinaatti

        //animaaation hidastuslaskurin muuttujat
        int figa2Hidastaja;
        int figa2Hidastajanraja = 5;

        //liikkumisen tilamuuttujat
        bool eteenpain = true; //ohjaus F-näppäin
        bool peruutus = false; // ohjaus B-näppäin
        bool liikkeella = false; //true kun vasen tai oikea nuolinäppäin on painettuna
        bool ylos = false;
        bool alas = false;

        int n = 1; //nopeusmuuttuja

        //rotaation kääntöpisteen arvot, ohjataan X, Z, Y ja T näppäimillä
        //aluksi keskipiste 80x120 kokoiselle osaspritelle

        float xpoint = 40f;
        float ypoint = 60f;
        float rot = 0f; //asteina, ohjataan R ja E näppäimillä
 

        public Figa1(Game game) : base(game)
        {
        }

        public void InitFiga1()
        {
            figa1 = new Texture2D(GraphicsDevice, 800, 600);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            paikka = new Vector2(200f, 257f);
        }

        public void Liiku()
        {
        }

        public override void Update(GameTime gameTime)
        {
            figa2Hidastaja++;
            if (eteenpain)
            {
                if (figa2Hidastaja > figa2Hidastajanraja)
                {
                    figa2X -= 80;
                    if (figa2X < 80) figa2X = 320;
                    figa2Hidastaja = 0;
                }
            }

            rect.X = (int)paikka.X;
            rect.Y = (int)paikka.Y;

            base.Update(gameTime);
        }
         

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            // Taustan väri 
            //GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            spriteBatch.Begin();


            //spriteBatch.Draw(ritari_anim, paikka, Color.White); //testattu että kuva näkyy yleensä

            string viesti = "Tervehdys!";

            //Vector2 alkupaikka = omaFontti.MeasureString(viesti);
            //spriteBatch.DrawString(omaFontti, viesti, new Vector2((naytonLeveys - alkupaikka.X) / 2, naytonKorkeus / 2), Color.White); //tekstin tulostus
            //spriteBatch.Draw(ritari_anim, new Rectangle((int) paikka.X, (int) paikka.Y, 160, 240), new Rectangle(ritari_x - 80, 0, 80, 120), Color.White); //koko suurennettu 2-kertaiseksi
            //spriteBatch.Draw(ritari_anim, paikka, new Rectangle(ritari_x - 80, 0, 80, 120), Color.White); //spritetsheet-animaatio yksinkertaisesti
            spriteBatch.Draw(figa1, paikka, new Rectangle(figa2X - 80, 0, 80, 120), Color.White); //spritetsheet-animaatio yksinkertaisesti    
            //spriteBatch.Draw(prinsessa, paikka, new Rectangle(ritari_x - 80, 0, 80, 120), Color.White, 0, new Vector2(xpoint, ypoint), 1f, SpriteEffects.None, 0f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
