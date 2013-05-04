using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
namespace IamYourShild
{
    /**
     * シーン管理クラス
     **/
    class SceneManager
    {
        /**
         * 面罵変数
         */
        Dictionary<String, GameScene> scenes;
        String current_state;
        Dictionary<string, SoundEffect> soundList;
        Dictionary<string, SoundEffectInstance> soundInstanceList;
        public Dictionary<string, UiObject> buttonList;
        public static Input Input = new Input();
        public Song gameBgm, titleBgm;

        /**
         * コンストラクタ
         **/
        public SceneManager()
        {
           
            scenes = new Dictionary<string, GameScene>();
            this.soundList = new Dictionary<string, SoundEffect>();
            this.soundInstanceList = new Dictionary<string, SoundEffectInstance>();
        }

        /**
         * 現在シーンの名称
         */
        public String CurrentState 
        {
            get { return current_state; }
            set { current_state = value; }
        }

        public GameScene currentScene
        {
            get { return scenes[current_state]; }
        }

        public void AddScene(GameScene addScene, String sceneName)
        {
            scenes.Add(sceneName, addScene);
        }

        public bool RemoveScene(String removeScene)
        {
            return scenes.Remove(removeScene);
        }

        public void LoadContent(ContentManager content)
        {

            foreach (GameScene scene in scenes.Values)
            {
                scene.LoadContent();
            }
            List<string> soundEffectList = this.getSoundEffectList();
            foreach (string soundEffectCode in soundEffectList)
            {
                SoundEffect soundWork = content.Load<SoundEffect>("Sound\\SE\\" + soundEffectCode);
                this.soundList.Add(soundEffectCode, soundWork);
            }
            List<string> soundEffectInstanceList = getSoundInstanceList();
            foreach (string soundEffectCode in soundEffectInstanceList)
            {
            //    SoundEffect soundWork = content.Load<SoundEffect>(soundEffectCode);
            //    this.soundInstanceList.Add(soundEffectCode, soundWork.CreateInstance());
            }
            List<string> exitButtonList = new List<string>();
            exitButtonList.Add("exit");
            exitButtonList.Add("exit-o");
            MediaPlayer.Volume = 0.7f;
            // ui一覧を定義
            buttonList = new Dictionary<string, UiObject>();
            buttonList.Add("exit", new UiObject(this.currentScene, Color.White, new Vector2(650, 540), "exit"));
            buttonList.Add("normal", new UiObject(this.currentScene, Color.White, new Vector2(650, 520), "normal"));
            buttonList.Add("hard", new UiObject(this.currentScene, Color.White, new Vector2(650, 520), "hard"));
            buttonList.Add("back", new UiObject(this.currentScene, Color.White, new Vector2(410, 540), "back"));
            buttonList.Add("next", new UiObject(this.currentScene, Color.White, new Vector2(530, 540), "next"));
            buttonList["exit"].LoadContent(exitButtonList);
            buttonList["exit"].rectData = new Rectangle(buttonList["exit"].rectData.X, buttonList["exit"].rectData.Y, 118, 43);
            buttonList["exit"].setButtonKeyWord("exit-o", "exit");
        }

        public SoundEffect selectSoundEffect(string soundEffectCode)
        {
            return this.soundList[soundEffectCode];
        }

        public SoundEffectInstance selectSoundEffectInstance(string soundEffectCode)
        {
            return this.soundInstanceList[soundEffectCode];
        }

        public void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (GameScene scene in scenes.Values)
            {
                scene.unLoadContent();
            }
        }

        public void Update(GameTime gameTime)
	    {
	        GameScene scene;
            CMouseState.Update();
	 
	        //現在のstateがある場合、保存されているGameStateへの参照を得る
	        if (scenes.TryGetValue(current_state, out scene))
	        {
	            // 現在のシーンを更新
	            scene.Update(gameTime);
	        }
            Input.Update();
	    }

        public void Draw()
        {
            GameScene scene;

            //現在のstateがある場合、保存されているGameStateへの参照を得る
            if (scenes.TryGetValue(current_state, out scene))
            {
                // 現在のシーンを更新
                scene.Draw();
            }
 
        }

        private List<string> getSoundEffectList()
        {
            List<string> soundEffectList = new List<string>();
            soundEffectList.Add("select");
            soundEffectList.Add("decide");
            return soundEffectList;
        }

        private List<string> getSoundInstanceList()
        {
            List<string> soundInstanceList = new List<string>();
            soundInstanceList.Add("clear");
            soundInstanceList.Add("stageclear");
            soundInstanceList.Add("gameover");
            soundInstanceList.Add("playball");
            soundInstanceList.Add("title");
            soundInstanceList.Add("boss");
            return soundInstanceList;
        }

    }
}
