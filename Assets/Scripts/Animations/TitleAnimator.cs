using UnityEditor.Animations;
using UnityEngine;

public class TitleAnimator : MonoBehaviour
{
    private static TitleAnimator _instance;
    public static TitleAnimator Instance => _instance;

    public AnimationClip startAnimationClip;
    public AnimationClip endAnimationClip;

    /// <summary>
    /// How much seconds the text should stay on screen (without in & out animations)
    /// </summary>
    public float delay;

    public bool isPlaying = false;

    private Animator _animator;
    private static readonly int StartTrigger = Animator.StringToHash(StartTriggerName);
    private const string StartTriggerName = "StartTrigger";

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        AnimatorController animatorController = new AnimatorController();
        animatorController.name = "AnimatorController";

        // Add the AnimatorController to the animator
        _animator.runtimeAnimatorController = animatorController;

        // Create a root state machine
        animatorController.AddLayer("BaseLayer");
        AnimatorStateMachine rootStateMachine = animatorController.layers[0].stateMachine;
        rootStateMachine.name = "RootStateMachine";

        // Add trigger parameters
        animatorController.AddParameter(StartTriggerName, AnimatorControllerParameterType.Trigger);

        // Create entry state
        AnimatorState entryState = rootStateMachine.AddState("Entry");
        entryState.motion = null; // You can assign an entry animation clip if needed

        // Create start animation state
        AnimatorState startAnimationState = rootStateMachine.AddState("StartAnimation");
        startAnimationState.motion = startAnimationClip;

        // Create end animation state
        AnimatorState endAnimationState = rootStateMachine.AddState("EndAnimation");
        endAnimationState.motion = endAnimationClip;

        // Create transitions between states
        AnimatorStateTransition startTransition = entryState.AddTransition(startAnimationState);
        startTransition.AddCondition(AnimatorConditionMode.If, 0, StartTriggerName);

        AnimatorStateTransition startToEndTransition = startAnimationState.AddTransition(endAnimationState);
        startToEndTransition.hasExitTime = true;
        startToEndTransition.exitTime = startAnimationClip.length + delay;

        AnimatorStateTransition endTransition = endAnimationState.AddTransition(entryState);
        endTransition.hasExitTime = true;
        endTransition.exitTime = endAnimationClip.length;

        // add end events to the clips
        AddEventToClip(endAnimationClip, "FinishedPlaying");
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void AddEventToClip(AnimationClip clip, string functionName, float time = -1f)
    {
        if (time == -1f)
            time = clip.length;
        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.functionName = functionName;
        animationEvent.time = time;
        clip.AddEvent(animationEvent);
    }

    public void StartAnimation()
    {
        if (!isPlaying)
        {
            _animator.SetTrigger(StartTrigger);
            isPlaying = true;
        }
    }

    private void FinishedPlaying(AnimationEvent animationEvent)
    {
        isPlaying = false;
        _animator.ResetTrigger(StartTrigger);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartAnimation();
        }
    }
}