using UnityEngine;

public class BoredBehaviour : StateMachineBehaviour
{

    [SerializeField]
    private float _timeUntilBored;

    [SerializeField]
    private int _numberOfBoredAnimations;

    private bool _isBored;
    private float _idleTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_isBored) { 
         _idleTime += Time.deltaTime;
            if (_idleTime >= _timeUntilBored)
            {
                int boredAnimationIndex = Random.Range(1, _numberOfBoredAnimations+1);
                animator.SetFloat("BoredAnimation", boredAnimationIndex);
                _isBored = true;
            }
         }
        else if (stateInfo.normalizedTime % 1 > 0.98)
        {
            ResetIdle(animator);
        }
    }

    private void ResetIdle(Animator animator)
    {
        _isBored = false;
        _idleTime = 0f;
        animator.SetFloat("BoredAnimation", 0f);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
