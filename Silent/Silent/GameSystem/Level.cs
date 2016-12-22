﻿using OpenTK;
using Silent.Entities;
using Silent.Graphics.RenderEngine;
using Silent.Graphics.Shaders;
using Silent.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.GameSystem
{
    public class Silent_Level
    {

        private static int levelCount = 0;
        public string LevelName = "SampleLevel"+levelCount.ToString();

        public Silent_Level(string name = null) {
            levelCount += 1;
        }

        public Silent_Input inputManager;

        public Camera currentCamera = new Camera();

        public List<Entity> entities = new List<Entity>();
        public List<Light> lights = new List<Light>();

        public StaticShader shader;
        private GLRenderer renderer = new GLRenderer(); 


        public virtual void OnLoad() { }
        public virtual void OnUpdate() { }
        public virtual void OnRender() { }
        public virtual void OnClosing() { }
        public virtual void OnClosed() { }
        public virtual void OnUnload() { }


        public void OnLoadLevel()
        {

            shader = new StaticShader();
            OnLoad();
            foreach (Entity entity in entities)
            {
                if (entity.Active)
                {
                    Console.WriteLine("Loading Entity: ", entity.EntityName);
                    entity.OnLoadEntity();
                }
            }

        }

        public void OnUpdateLevel()
        {
            OnUpdate();

            foreach (Entity entity in entities)
            {
                if (entity.Active)
                {
                    entity.OnUpdateEntity();
                }
            }
            
        }

        public void OnRenderLevel()
        {
            OnRender();

            //TODO: fix the way entities are added
            foreach (Entity entity in entities)
            {
                if (entity.Visible)
                {
                    renderer.PrepareToRender();
                    shader.StartShader();
                    shader.LoadLight(lights[0]);
                    shader.LoadToViewMatrix(currentCamera.view);
                    renderer.Render(entity,shader);
                    entity.OnRenderEntity();
                    shader.StopShader();
                }
            }

        }

        public void OnClosingLevel()
        {
            OnClosing();

            foreach (Entity entity in entities)
            {
                if (entity.Active)
                {
                    entity.OnClosingEntity();
                }
            };

        }

        public void OnClosedLevel()
        {
            OnClosed();

            foreach (Entity entity in entities)
            {
                if (entity.Active)
                {
                    entity.OnClosedEntity();
                }
            }

        }

        public void OnUnloadLevel()
        {
            OnUnload();

            foreach (Entity entity in entities)
            {
                if (entity.Active)
                {
                    entity.OnDeleteEntity();
                }
            }

        }

        public string GetLevelName()
        {
            if (LevelName == null)
            {
                Console.WriteLine("You have to set the level name first");
            }
            return LevelName;
        }

        public void SetLevelName(string newLevelName)
        {
            LevelName = newLevelName;
        }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public void AddLight(Light light)
        {
            lights.Add(light);
        }

    }
}
