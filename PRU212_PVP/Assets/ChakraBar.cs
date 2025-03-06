using UnityEngine;
using UnityEngine.UI;
public class ChakraBar : MonoBehaviour
{
    public Slider chakraBar;
    public void SetMaxchakra(int chakra)
    {
        chakraBar.maxValue = chakra;
        chakraBar.value = chakra;
    }
    public void Setchakra(int chakra)
    {
        chakraBar.value = chakra;
    }
}
