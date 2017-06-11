﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMOFGameEngine.Sound
{
    public class SoundManager
    {
        List<GameSound> soundLst;
        GameSound currentSound;
        public GameSound CurrentSound
        {
            get { return currentSound; }
            set { currentSound = value; }
        }

        public SoundManager()
        {
            soundLst = new List<GameSound>();
            currentSound = null;
        }

        public void Init()
        {
            GameSound sound1 = new GameSound("./Music/vivaldi_winter_allegro.ogg");
            sound1.SoundType = SoundType.MainMenu;
            soundLst.Add(sound1);
        }

        public void ChangeSoundStateToType(SoundType soundType)
        {
            switch (soundType)
            {
                case SoundType.MainMenu:
                    var result = from sound in soundLst
                                 where sound.SoundType == SoundType.MainMenu
                                 select sound;
                    if (result.Count() == 1)
                    {
                        if (CurrentSound != null)
                        {
                            CurrentSound.Stop();
                        }
                        GameSound sound = result.First();
                        sound.Play();
                    }
                    break;
                case SoundType.Scene:
                    var result2 = from sound in soundLst
                                 where sound.SoundType == SoundType.Scene
                                 select sound;
                    if (result2.Count() > 0)
                    {
                        if (CurrentSound != null)
                        {
                            CurrentSound.Stop();
                        }
                        int randIndex = new Random().Next(0, result2.Count() - 1);
                        GameSound sound = result2.ElementAt(randIndex);
                        sound.Play();
                    }
                    break;
            }
        }

        public void ChangeSoundStateToSound(string soundID)
        {
            var result = from sound in soundLst
                         where sound.ID == soundID
                         select sound;
            if (result.Count() == 1)
            {
                if (CurrentSound != null)
                {
                    CurrentSound.Stop();
                }
                GameSound sound = result.First();
                sound.Play();
            }
        }
    }
}