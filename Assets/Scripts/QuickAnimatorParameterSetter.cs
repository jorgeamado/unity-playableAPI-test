using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class QuickAnimatorParameterSetter : MonoBehaviour
{
	public KeyCodeAnimatorParameter[] Keys;
	public Animator _a;
	// Use this for initialization
	void Start ()
	{
		_a = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if (_a == null)
		{
			_a = GetComponent<Animator>();
			return;
		}

		foreach (var k in Keys)
		{
			k.ProcessInput(_a);
		}
	}
}


[System.Serializable]
public class KeyCodeAnimatorParameter
{
	public KeyCode PositiveKeyCode;
	public KeyCode NegativeKeyCode;
	public string Parameter;

    private Action PositiveAction;
    private Action NegativeAction;
    [SerializeField] private bool _isSetted;


    public void ProcessInput(Animator animator)
    {
        if (!_isSetted)
        {
            SetupActions(animator);
        }

        if (Input.GetKeyUp(PositiveKeyCode))
            PositiveAction();
        else if (Input.GetKeyUp(NegativeKeyCode))
            NegativeAction();

    }

    private void SetupActions(Animator animator)
    {
        var animparameter = animator.parameters.FirstOrDefault(parameter => parameter.name == Parameter);
        if (animparameter != null)
        {
            if (animparameter.type == AnimatorControllerParameterType.Bool)
                SetupBoolActions(animator);
        }
    }

    private void SetupBoolActions(Animator animator)
    {
        PositiveAction = () => animator.SetBool(Parameter, true);
        NegativeAction = () => animator.SetBool(Parameter, false);
        _isSetted = true;
    }
}
