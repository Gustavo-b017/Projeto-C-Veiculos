CP 2
```
import RPi.GPIO as GPIO
import time

# --- Pinos ---
LED_VERDE    = 6
LED_VERMELHO = 26

BOTAO_1      = 20
BOTAO_2      = 21

# --- Senha correta: sequência de 1s e 2s ---
SENHA_CORRETA = [1, 1, 2, 2, 2]

# --- Setup ---
GPIO.setmode(GPIO.BOARD)
GPIO.setwarnings(False)

GPIO.setup(BOTAO_1,      GPIO.IN,  pull_up_down=GPIO.PUD_UP)
GPIO.setup(BOTAO_2,      GPIO.IN,  pull_up_down=GPIO.PUD_UP)
GPIO.setup(LED_VERDE,    GPIO.OUT)
GPIO.setup(LED_VERMELHO, GPIO.OUT)

# --- Funções auxiliares ---
def botao_pressionado(pino: int) -> bool:
    if GPIO.input(pino) == GPIO.LOW:
        time.sleep(0.05)  # debounce
        if GPIO.input(pino) == GPIO.LOW:
            return True
    return False

def acender_verde() -> None:
    GPIO.output(LED_VERDE,    GPIO.HIGH)
    GPIO.output(LED_VERMELHO, GPIO.LOW)

def acender_vermelho() -> None:
    GPIO.output(LED_VERMELHO, GPIO.HIGH)
    GPIO.output(LED_VERDE,    GPIO.LOW)

def apagar_leds() -> None:
    GPIO.output(LED_VERDE,    GPIO.LOW)
    GPIO.output(LED_VERMELHO, GPIO.LOW)

# --- Loop principal ---
print("Cofre iniciado. Digite a senha...")

sequencia: list[int] = []

while True:

    if botao_pressionado(BOTAO_1):
        sequencia.append(1)
        print(f"Botão 1 | Sequência: {sequencia}")
        time.sleep(0.3)

    elif botao_pressionado(BOTAO_2):
        sequencia.append(2)
        print(f"Botão 2 | Sequência: {sequencia}")
        time.sleep(0.3)

    if len(sequencia) == len(SENHA_CORRETA):
        if sequencia == SENHA_CORRETA:
            print("SENHA CORRETA! Cofre aberto.")
            acender_verde()
        else:
            print("SENHA INCORRETA! Tente novamente.")
            acender_vermelho()

        time.sleep(2)
        apagar_leds()
        sequencia = []
        print("Digite a senha novamente...")
```
