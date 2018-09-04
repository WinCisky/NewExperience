# Two
### Tentativo di creazione di un gioco VR che sia ottimizzato a livello di performance
Questo progetto prevede lo sviluppo di un gioco che sia giocabile con l'uso di un supporto per la realtà virtuale ma non solo.
Deve essere possibile un'esperienza di gioco che non necessiti di un visore, infatti questo dovrebbe rendere l'esperienza solamente più immersiva.
Con ottimizzazione a livello di performace viene inteso l'utilizzo delle 2 nuove metodologie di sviluppo software usabili in Unity: Pure ECS & Hybrid ECS


### Concept
Il gioco si divide in fasi che il giocatore deve superare. Alla fine di ogni fase è previsto l'avanzamento automatico alla fase successiva ma questo non preclude il fatto che la fase precedente non dovrà essere ripetuta.
Il giocatore controlla una navicella attraverso l'inclinazione del suo sguardo, tutti gli input vengono effettuati con questo metodo e non si necessitano iterazioni dirette con lo schermo.
I vari livelli sono i seguenti:
1) Campo di asteroidi
2) Campro di satelliti
3) Campo di asteroidi nei pressi di un pianeta
4) Fuga da navicelle nemiche
5) Caccia a navicelle nemiche
6) Battaglia contro la base nemica
