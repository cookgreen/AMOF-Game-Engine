﻿using System;
using System.Collections.Generic;
using System.Text;
using Mogre;
using MOIS;
using Mogre_Procedural;
using Mogre_Procedural.MogreBites;
using NVorbis;

namespace AMOFGameEngine
{
    class AdvancedMogreFramework 
    {
        public Root m_Root;
        public RenderWindow m_RenderWnd;
        public Viewport m_Viewport;
        public Log m_Log;
        public Timer m_pTimer;

        public MOIS.InputManager m_InputMgr;
        public Keyboard m_Keyboard;
        public Mouse m_Mouse;

        public SdkTrayManager m_TrayMgr;

        public static string LastStateName;

        //public NVorbis.NAudioSupport.VorbisWaveReader m_pVorbis;
        public NAudio.Vorbis.VorbisWaveReader m_Vorbis;
        public NAudio.Wave.WaveOut m_WaveOut;

        public MQuickGUI.GUIManager m_Gui;
        
        public AdvancedMogreFramework()
        {
            m_Root = null;
            m_RenderWnd = null;
            m_Viewport = null;
            m_Log = null;
            m_pTimer = null;

            m_InputMgr = null;
            m_Keyboard = null;
            m_Mouse = null;
            m_TrayMgr = null;
            m_Gui = null;
         }

        public bool initOgre(String wndTitle)
        {
            LogManager logMgr = new LogManager();
 
            m_Log = LogManager.Singleton.CreateLog("OgreLogfile.log", true, true, false);
            m_Log.SetDebugOutputEnabled(true);
 
            m_Root = new Root();
 
            if(!m_Root.ShowConfigDialog())
                return false;
               m_RenderWnd = m_Root.Initialise(true, wndTitle);
 
            m_Viewport = m_RenderWnd.AddViewport(null);
            ColourValue cv=new ColourValue(0.5f,0.5f,0.5f);
            m_Viewport.BackgroundColour=cv;
 
            m_Viewport.Camera=null;
 
            int hWnd = 0;
            //ParamList paramList;
            m_RenderWnd.GetCustomAttribute("WINDOW", out hWnd);
 
            m_InputMgr = InputManager.CreateInputSystem((uint)hWnd);
            m_Keyboard = (MOIS.Keyboard)m_InputMgr.CreateInputObject(MOIS.Type.OISKeyboard, true);
            m_Mouse =  (MOIS.Mouse)m_InputMgr.CreateInputObject(MOIS.Type.OISMouse, true);

            m_Mouse.MouseMoved+=new MouseListener.MouseMovedHandler(mouseMoved);
            m_Mouse.MousePressed += new MouseListener.MousePressedHandler(mousePressed);
            m_Mouse.MouseReleased += new MouseListener.MouseReleasedHandler(mouseReleased);

            m_Keyboard.KeyPressed += new KeyListener.KeyPressedHandler(keyPressed);
            m_Keyboard.KeyReleased += new KeyListener.KeyReleasedHandler(keyReleased);

            MOIS.MouseState_NativePtr mouseState = m_Mouse.MouseState;
                mouseState.width = m_Viewport.ActualWidth;
                mouseState.height = m_Viewport.ActualHeight;
            //m_pMouse.MouseState = tempMouseState;

 
            String secName, typeName, archName;
            ConfigFile cf=new ConfigFile();
            cf.Load("resources.cfg","\t:=",true);
 
            ConfigFile.SectionIterator seci = cf.GetSectionIterator();
            while (seci.MoveNext())
            {
                secName = seci.CurrentKey;
                ConfigFile.SettingsMultiMap settings = seci.Current;
                foreach (KeyValuePair<string, string> pair in settings)
                {
                    typeName = pair.Key;
                    archName = pair.Value;
                    ResourceGroupManager.Singleton.AddResourceLocation(archName, typeName, secName);
                }
            }
            TextureManager.Singleton.DefaultNumMipmaps=5;
            ResourceGroupManager.Singleton.InitialiseAllResourceGroups(); 
 
            m_TrayMgr = new SdkTrayManager("AOFTrayMgr", m_RenderWnd, m_Mouse, null);
 
            m_pTimer = new Timer();
            m_pTimer.Reset();
 
            m_RenderWnd.IsActive=true;
            m_Gui = new MQuickGUI.GUIManager();
            return true;
        }
        public void updateOgre(double timeSinceLastFrame)
        {
        }

        public bool keyPressed(KeyEvent keyEventRef)
        {
             if(m_Keyboard.IsKeyDown(MOIS.KeyCode.KC_V))
            {
                m_RenderWnd.WriteContentsToTimestampedFile("AMOF_Screenshot_", ".jpg");
                return true;
            }
 
            if(m_Keyboard.IsKeyDown(MOIS.KeyCode.KC_O))
            {
                if(m_TrayMgr.isLogoVisible())
                {
                    m_TrayMgr.hideFrameStats();
                    m_TrayMgr.hideLogo();
                }
                else
                {
                    m_TrayMgr.showFrameStats(TrayLocation.TL_BOTTOMLEFT);
                    m_TrayMgr.showLogo(TrayLocation.TL_BOTTOMRIGHT);
                }
            }
 
            return true;
        }
        public bool keyReleased(KeyEvent keyEventRef)
        {
            return true;
        }

        public bool mouseMoved(MouseEvent evt)
        {
            return true;
        }
        public bool mousePressed(MouseEvent evt, MouseButtonID id)
        {
            return true;
        }
        public bool mouseReleased(MouseEvent evt, MouseButtonID id)
        {
            return true;
        }
        public float Clamp(float val, float minval, float maxval)
        {
            return System.Math.Max(System.Math.Min(val, maxval), minval);
        }
        public static AdvancedMogreFramework instance;
        public static AdvancedMogreFramework Singleton
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdvancedMogreFramework();
                }
                return instance;
            }
        }
    }
}
