# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Контекст проекта

PSX-Horror — шутер от первого/третьего лица в стиле PS1-хоррора на Unity 6 (`6000.3.17f1`). Стек:

- **DI** — VContainer (`1.18.0`, подключён через Git URL). Scopes: `ProjectScope` (singletons), `GameScope` (scoped).
- **Ввод** — new Input System (`1.19.0`). Action map генерируется из `Assets/Settings/ActionMap.inputactions` → `ActionMap.cs`. Switch между `Player` и `UI` режимами через `IInputService.SetInput()`.
- **Анимации** — DOTween (`DG.Tweening`). Используется в DoorView для открытия/закрытия.
- **Текст** — TextMesh Pro (TMP).
- **Рендеринг** — URP (`17.3.0`).
- **Камера** — Cinemachine (`3.1.7`).
- **Физика движения** — `CharacterController` (PSX-style: без ускорения, direct motion).
- **IDE** — Rider + VS Code параллельно. VS Code — хост для Claude Code; Rider — основной редактор.

> При добавлении новой технологии в стек — расширяй этот раздел. Если видишь в коде технологию, которой нет здесь — спроси.

## Структура кода

`Assets/Scripts/` — весь пользовательский код:

```
Assets/Scripts/
├── Core/
│   ├── Audio/          # IAudioService, AudioService (заглушка)
│   ├── GameState/      # IGameState, GameStateMachine (ITickable), состояния, GameEntryPoint
│   ├── Input/          # IInputService, InputService, InputType enum
│   └── Scene/          # ISceneLoader, SceneLoader (заглушка)
├── DI/
│   ├── GameScope.cs    # Game-скоп: InputService, GameStateMachine, PlayerSpawner, GameEntryPoint
│   └── ProjectScope.cs # Project-скоп: AudioService, SceneLoader
├── Interaction/
│   ├── IInteractable.cs
│   ├── IInteractor.cs
│   ├── PlayerInteractor.cs      # SphereCast детекция, вызов Interact
│   ├── InteractorView.cs        # TMP-подсказка взаимодействия
│   └── Interactables/
│       ├── DoorController.cs    # Чистая логика двери (IsOpen, Toggle)
│       └── DoorView.cs          # MonoBehaviour + IInteractable, DOTween анимация
├── Player/
│   ├── IPlayerSpawner.cs
│   ├── PlayerSpawner.cs         # VContainer resolver.Instantiate
│   └── PlayerMovement.cs        # CharacterController + CinemachineCamera, FixedUpdate movement
└── UI/
    ├── PauseMenu.cs
    └── MainMenu.cs
```

`Assets/Settings/` — Input System actions (`ActionMap.inputactions` + сгенерированный `ActionMap.cs`).

## Паттерны и конвенции

**DI:**
- `ProjectScope` — `Lifetime.Singleton` (глобальные сервисы: Audio, SceneLoader).
- `GameScope` — `Lifetime.Scoped` (игровые сервисы: Input, StateMachine, Spawner).
- MonoBehaviour → `[Inject] private void Construct(IInterface dep)`.
- Pure C# классы → конструкторная инъекция.

**Game State Machine:**
- `IGameState` с `Enter()`, `Tick()`, `Exit()`.
- `GameStateMachine` реализует `ITickable` (автоматически тикается VContainer).
- Состояния: `GameplayState`, `PauseState`, `GameoverState` (пусты, scaffolding).

**Ввод:**
- `IInputService` — фасад; switching через `SetInput(InputType.Player | InputType.UI)`.
- В Player режиме: курсор locked, UI — unlocked.
- В MonoBehaviour input callbacks (`OnMove`, `OnSprint`) — стандартный Input System паттерн.

**Interaction (IInteractor / IInteractable):**
- `IInteractable` несёт `CanInteract`, `Interact`, `Hint`, `Transform`.
- Разделение View/Controller: `DoorController` (чистая логика) → `DoorView` (MonoBehaviour + DOTween).
- `PlayerInteractor` — SphereCastAll детекция в `Update()` (не FixedUpdate!).
- `PlayerInteractorView` — подписывается на `OnStartView`/`OnExitView` и показывает TMP-текст.

**PlayerMovement:**
- `CharacterController.Move` в `FixedUpdate`.
- Гравитация: кастомная (не CharacterController-builtin).
- Направление движения относительно камеры (`camera.transform.forward/right` с обнулением Y).
- `CinemachineCamera` для взгляда (не bline-based look).

**Стиль:**
- Namespace — `NLB.<Layer>` (Core, DI, Player, Interaction, UI).
- Интерфейсы — `I` префикс.
- `[field: SerializeField]` для auto-property сериализации.
- `#region` для группировки Member'ов.
- Git-коммиты — на русском.

## Что нельзя менять без обсуждения

- Существующий `IGameStateMachine` API (`ChangeState`, `CurrentState`).
- `IInteractor` / `IInteractable` контракты (events + методы).
- `InputType` enum и `IInputService` публичный API.
- DI-регистрации в `GameScope`/`ProjectScope`.

## Что в процессе / scaffolding

- `AudioService`, `SceneLoader` — пустые реализации.
- `PauseMenu`, `MainMenu` — пустые/минимум кода.
- `GameplayState`, `PauseState`, `GameoverState` — пустые состояния.
