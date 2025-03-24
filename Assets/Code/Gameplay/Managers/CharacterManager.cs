using Database;
using Database.Character;
using Gameplay.Character;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Management.Characters
{
    public class CharacterManager : GameplayManager<CharacterManager>
    {
        #region VARIABLES

        [SerializeField, ValueDropdown(CharactersDatabase.GET_CHARACTERS_DATA_METHOD)] private int defaultPlayerCharacterDataId;
        [SerializeField, ReadOnly] private PlayerInGame player;

        [SerializeField] private List<CharacterInGame> characters = new();
        private Dictionary<CharacterInGame, bool> charactersTmp = new();

        #endregion

        #region PROPERTIES

        [ShowInInspector, ReadOnly]
        public PlayerInGame Player
        {
            get
            {
                if (player == null)
                    player = characters.Find(x => x is PlayerInGame) as PlayerInGame;
                return player;
            }
            set
            {
                player = value;
            }
        }

        #endregion

        #region UNITY_METHODS

        private void Update()
        {
            foreach (var characterTmp in charactersTmp)
            {
                if (characterTmp.Value)
                    characters.Add(characterTmp.Key);
                else
                    characters.Remove(characterTmp.Key);
            }

            charactersTmp.Clear();

            //for (int i = 0; i < characters.Count; i++)
            //    characters[i].OnUpdate();
        }

        #endregion

        #region METHODS

        public override void LateInitialzie()
        {
            base.LateInitialzie();

            if (player == null)
                CreatePlayer();
        }

        public override void CleanUp()
        {
            //Only detach events. Dont clean up character for save values to serialization
            foreach (var character in characters)
                character.DetachEvents();

            base.CleanUp();
        }

        /// <summary>
        /// Spawn setted character
        /// </summary>
        /// <typeparam name="T">Type of character like player or npc, enemy itc.</typeparam>
        /// <param name="dataId">Id of character data into CharacterDatabase</param>
        /// <returns></returns>
        public T CreateCharacter<T>(int dataId) where T : CharacterInGame, new()
        {
            CharacterData data = MainDatabases.Instance.CharactersDatabase.GetItemData(dataId);
            return CreateCharacter<T>(data);
        }

        /// <summary>
        /// Spawn setted character
        /// </summary>
        /// <typeparam name="T">Type of character like player or npc, enemy itc.</typeparam>
        /// <param name="data">Base data for character</param>
        /// <returns></returns>
        public T CreateCharacter<T>(CharacterData data) where T : CharacterInGame, new()
        {
            T character = new T();

            character.SetData(data);
            character.Initialize();
            character.AttachEvents();

            return character;
        }

        public void RemoveCharacter<T>(T character) where T : CharacterInGame
        {
            character.CleanUp();
            character.DetachEvents();
            charactersTmp.Add(character, false);
        }

        [Button]
        private void CreatePlayer()
        {
            player = CreateCharacter<PlayerInGame>(defaultPlayerCharacterDataId);
        }

        #endregion
    }
}