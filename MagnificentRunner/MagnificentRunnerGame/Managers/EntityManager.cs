using System.Collections.Generic;
using MagnificentRunner.MagnificentRunnerGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MagnificentRunnerGame.Managers
{
    public class EntityManager
    {
        private readonly HashSet<IEntity> _entities;

        public EntityManager() => _entities = new HashSet<IEntity>();

        public void AddEntity(IEntity entity) => _entities.Add(entity);
        
        public void RemoveEntity(IEntity entity) => _entities.Remove(entity);
        
        public void UpdateEntities(GameTime gameTime) 
        {
            foreach (var entity in _entities)
                entity.Update(gameTime);
        }

        public void DrawEntities(SpriteBatch spriteBatch, GameTime gameTime) 
        {
            foreach (var entity in _entities)
                entity.Draw(spriteBatch, gameTime);
        }
    }
}