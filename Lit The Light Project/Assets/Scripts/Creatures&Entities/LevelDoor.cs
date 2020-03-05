using UnityEngine;

public class LevelDoor : MonoBehaviour
{
    public static EmptyEvent onWithPlayerCollided = new EmptyEvent();

    [SerializeField] private Sprite openedSprite;
    [SerializeField] private Sprite emptySprite;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        GameManager.onDoorOpened.AddListener(Open);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Open(bool isLastLevel)
    {
        spriteRenderer.sprite = isLastLevel ? emptySprite : openedSprite;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>())
        {
            onWithPlayerCollided?.Invoke();
        }
    }

}
