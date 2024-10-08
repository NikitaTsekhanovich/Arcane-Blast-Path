using GameControllers.GameData;
using GameControllers.MonoBehHandlers;
using GameControllers.Systems;
using GameControllers.Systems.GameField;
using Leopotam.Ecs;
using LevelControllers;
using SceneLoader;
using StoreControllers.BackgroundStore;
using UnityEngine;
using Voody.UniLeo;

namespace GameControllers.Ecs
{
    public sealed class EcsGameStartup : MonoBehaviour
    {
        [SerializeField] private UIContainer _uiContainer;
        [SerializeField] private BlocksImagesContainer _blocksImagesContainer;
        [SerializeField] private SettingsGameData _settingsGameData;
        [SerializeField] private SoundsContainer _soundsContainer;
        private EcsWorld _world;
        private EcsSystems _systems;

        private void OnEnable()
        {
            SceneDataLoader.OnInitEcsWorld += InitEcsWorld;
        }

        private void OnDisable()
        {
            SceneDataLoader.OnInitEcsWorld -= InitEcsWorld;
        }

        public void InitEcsWorld(LevelData levelData, BackgroundData backgroundData)
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems.ConvertScene();

            var runTimeData = new RunTimeData();

            AddInjections(runTimeData, levelData, backgroundData);
            AddOneFrames();
            AddSystems();

            _systems.Init();
        }

        private void AddInjections(
            RunTimeData runTimeData, 
            LevelData levelData, 
            BackgroundData backgroundData)
        {
            _systems
                .Inject(_soundsContainer)
                .Inject(_settingsGameData)
                .Inject(runTimeData)
                .Inject(_blocksImagesContainer)
                .Inject(_uiContainer)
                .Inject(levelData)
                .Inject(backgroundData);
        }

        private void AddSystems()
        {
            _systems
                .Add(new LoadGameDataSystem())
                .Add(new EntityInitializeSystem())
                .Add(new BallSpawnerSystem())
                .Add(new CreatorGameFieldSystem())
                .Add(new InitializeBlockSystem())
                .Add(new InputSystem())
                .Add(new LineSystem())
                .Add(new FlyBallsDirectionSystem())
                .Add(new MovableSystem())
                .Add(new RotateSystem())
                .Add(new NumberBallsSystem())
                .Add(new LifeBlockSystem())
                .Add(new DestroyableBlockSystem())
                .Add(new BlockGraphicsSystem())
                .Add(new UpdaterGameFieldSystem())
                .Add(new ScoreSystem())
                .Add(new CoinSystem())
                .Add(new UseAbilitySmallBallsSystem())
                .Add(new UseAbilityMegaBallSystem())
                .Add(new GameOverSystem())
                .Add(new GameWinSystem())
                .Add(new SetSpawnPointSystem())
                .Add(new BackBallsSystem());
        }

        private void AddOneFrames()
        {
            
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            if (_systems == null) return;

            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}