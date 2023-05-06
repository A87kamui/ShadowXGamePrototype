using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Reveal : MonoBehaviour
{
    //Access to cheat code components
    public Button shift2Button;
    public Button shift3Button;
    public Button shift4Button;
    public Button plus2Button;
    public Button plus3Button;
    public Button k_Button;
    public Button lButton;
    public Button sButton;
    public Button destroyEnemyButton;
    public Button restoreHPButton;
    public Button maxShurikenButton;

    //Use by both
    public TextMeshProUGUI controlsText;

    //Acces to Option components
    public Button wButton;
    public Button shift1Button;
    public Button aButton;
    public Button dButton;
    public Button spaceButton;
    public Button leftClickButton;
    public Button rightClickButton;
    public Button walkButton;
    public Button slashButton;
    public Button runButton;
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;
    public Button meleeButton;
    public Button shurikenButton;

    //Created variables
    public bool show = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void revealCheatCodeButton()
    {
        if(!show)
        {
            show = true;
            controlsText.text = "Cheat Codes";

            shift2Button.gameObject.SetActive(true);
            shift3Button.gameObject.SetActive(true);
            shift4Button.gameObject.SetActive(true);
            plus2Button.gameObject.SetActive(true);
            plus3Button.gameObject.SetActive(true);
            k_Button.gameObject.SetActive(true);
            lButton.gameObject.SetActive(true);
            sButton.gameObject.SetActive(true);
            destroyEnemyButton.gameObject.SetActive(true);
            restoreHPButton.gameObject.SetActive(true);
            maxShurikenButton.gameObject.SetActive(true);

            wButton.gameObject.SetActive(false);
            shift1Button.gameObject.SetActive(false);
            aButton.gameObject.SetActive(false);
            dButton.gameObject.SetActive(false);
            spaceButton.gameObject.SetActive(false);
            leftClickButton.gameObject.SetActive(false);
            rightClickButton.gameObject.SetActive(false);
            walkButton.gameObject.SetActive(false);
            slashButton.gameObject.SetActive(false);
            runButton.gameObject.SetActive(false);
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
            jumpButton.gameObject.SetActive(false);
            meleeButton.gameObject.SetActive(false);
            shurikenButton.gameObject.SetActive(false);

        }
        else if (show)
        {
            show = false;
            controlsText.text = "Controls";

            shift2Button.gameObject.SetActive(false);
            shift3Button.gameObject.SetActive(false);
            shift4Button.gameObject.SetActive(false);
            plus2Button.gameObject.SetActive(false);
            plus3Button.gameObject.SetActive(false);
            k_Button.gameObject.SetActive(false);
            lButton.gameObject.SetActive(false);
            sButton.gameObject.SetActive(false);
            destroyEnemyButton.gameObject.SetActive(false);
            restoreHPButton.gameObject.SetActive(false);
            maxShurikenButton.gameObject.SetActive(false);

            wButton.gameObject.SetActive(true);
            shift1Button.gameObject.SetActive(true);
            aButton.gameObject.SetActive(true);
            dButton.gameObject.SetActive(true);
            spaceButton.gameObject.SetActive(true);
            leftClickButton.gameObject.SetActive(true);
            rightClickButton.gameObject.SetActive(true);
            walkButton.gameObject.SetActive(true);
            slashButton.gameObject.SetActive(true);
            runButton.gameObject.SetActive(true);
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
            jumpButton.gameObject.SetActive(true);
            meleeButton.gameObject.SetActive(true);
            shurikenButton.gameObject.SetActive(true);
        }
    }

    public void hidCheatCodeButton()
    {
        show = false;
        controlsText.text = "Controls";

        shift2Button.gameObject.SetActive(false);
        shift3Button.gameObject.SetActive(false);
        shift4Button.gameObject.SetActive(false);
        plus2Button.gameObject.SetActive(false);
        plus3Button.gameObject.SetActive(false);
        k_Button.gameObject.SetActive(false);
        lButton.gameObject.SetActive(false);
        sButton.gameObject.SetActive(false);
        destroyEnemyButton.gameObject.SetActive(false);
        restoreHPButton.gameObject.SetActive(false);
        maxShurikenButton.gameObject.SetActive(false);

        wButton.gameObject.SetActive(true);
        shift1Button.gameObject.SetActive(true);
        aButton.gameObject.SetActive(true);
        dButton.gameObject.SetActive(true);
        spaceButton.gameObject.SetActive(true);
        leftClickButton.gameObject.SetActive(true);
        rightClickButton.gameObject.SetActive(true);
        walkButton.gameObject.SetActive(true);
        slashButton.gameObject.SetActive(true);
        runButton.gameObject.SetActive(true);
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        jumpButton.gameObject.SetActive(true);
        meleeButton.gameObject.SetActive(true);
        shurikenButton.gameObject.SetActive(true);
    }
}
