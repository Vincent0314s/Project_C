using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum InputSetting {
    Forward,
    Backward,
    Left,
    Right,
    L
}

[System.Serializable]
public class ComboInput {
    public InputSetting input;

    public ComboInput(InputSetting _input) {
        input = _input;
    }

    public bool IsSameAs(ComboInput _input) {
        return input == _input.input;
    }
}

[System.Serializable]
public class Combo {
    public PlayerStateBehaviour playerState;
    public List<PlayerStateBehaviour> doActionDuringStates;
    public List<ComboInput> inputs;
    public AttackType type;
    public UnityEvent OnInputFinished;
    int currentInputIndex = 0;
    PlayerSpecialised ps;

    public void Init(PlayerSpecialised _ps)
    {
        ps = _ps;
    }

    bool CanDoState() {
        if (doActionDuringStates.Count > 0) {
            for (int i = 0; i < doActionDuringStates.Count; i++)
            {
                if (doActionDuringStates.Contains(ps.playerState))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CanContinueCombo(ComboInput _input) {
        if (CanDoState())
        {
            if (inputs[currentInputIndex].IsSameAs(_input))
            {
                currentInputIndex += 1;
                if (currentInputIndex >= inputs.Count)
                {
                    OnInputFinished?.Invoke();
                    currentInputIndex = 0;
                }
                return true;
            }
            else
            {
                currentInputIndex = 0;
                return false;
            }
        }
        else {
            return false;
        }
       
    }

    public ComboInput CurrentComboInput() {
        if (currentInputIndex > inputs.Count) {
            return null;
        }
        return inputs[currentInputIndex];
    }

    public void Reset() {
        currentInputIndex = 0;
    }
    
}

public class PlayerComboInput : MonoBehaviour
{
    [ArrayElementTitle("playerState")]
    public List<Combo> combos = new List<Combo>();
    public float comboInputTime = 0.5f;
    private float currentComboInputTime;

    private CharacterBaseValue cbv;
    private PlayerSpecialised ps;

    private ComboInput lastInput = null;
    private List<int> possibleCombos = new List<int>();
    bool skipFrame = false;


    void Start()
    {
        cbv = GetComponent<CharacterBaseValue>();
        ps = GetComponent<PlayerSpecialised>();
        BasicComboSetup();
    }

    void BasicComboSetup() {
        for (int i = 0; i < combos.Count; i++)
        {
            Combo c = combos[i];
            c.Init(ps);
            c.OnInputFinished.AddListener(()=> {
                skipFrame = true;
                cbv.anim.Play(c.playerState.ToString(),0,0);
                cbv.GetDamageFromAttackType(c.type);
                ps.SetStateBehaviour(c.playerState);
                ResetCombo();
            });
        }
    }

    void Update()
    {
        if (possibleCombos.Count > 0)
        {
            currentComboInputTime += Time.deltaTime;
            if (currentComboInputTime >= comboInputTime)
            {
                if (lastInput != null)
                {
                    lastInput = null;
                }
                ResetCombo();
            }
        }
        else {
            currentComboInputTime = 0;
        }

        ComboInput currentInput = null;
        if (ps.isMouseLeftClick)
            currentInput = new ComboInput(InputSetting.L);
        if (ps.isForwardKey)
            currentInput = new ComboInput(InputSetting.Forward);
        if (ps.isBackwardKey)
            currentInput = new ComboInput(InputSetting.Backward);
        if (ps.isTurnLeftKey)
            currentInput = new ComboInput(InputSetting.Left);
        if (ps.isTurnRightKey)
            currentInput = new ComboInput(InputSetting.Right);

        if (currentInput == null)
            return;

        lastInput = currentInput;

     
        List<int> failCombos = new List<int>();
        for (int i = 0; i < possibleCombos.Count; i++)
        {
            Combo c = combos[possibleCombos[i]];
            if (c.CanContinueCombo(currentInput))
            {
                currentComboInputTime = 0;
            }
            else {
                failCombos.Add(i);
            }
        }

        if (skipFrame)
        {
            skipFrame = false;
            return;
        }


        for (int i = 0; i < combos.Count; i++)
        {
            if (possibleCombos.Contains(i))
            {
                continue;
            }
            if (combos[i].CanContinueCombo(currentInput)) {
                possibleCombos.Add(i);
                currentComboInputTime = 0;
            }
        }

        if (failCombos.Count > 0) {
            foreach (int i in failCombos)
            {
                possibleCombos.RemoveAt(i);
            }
        }
      
        if (possibleCombos.Count <= 0) {
            //attack
        }
    }

    void ResetCombo() {
        currentComboInputTime = 0;

        for (int i = 0; i < possibleCombos.Count; i++)
        {
            Combo c = combos[possibleCombos[i]];
            c.Reset();
        }
        possibleCombos.Clear();
    }
}
