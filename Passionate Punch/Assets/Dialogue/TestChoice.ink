-> main

=== main ===
// Neye bakıyon lan kurabiye var simit var neye bakıyon!?
//    + [Hiç, bakıyorum öyle]
//        -> chosen("Faratın adamı mısın lan sen")
//    + [Geri bas lan geri bas]
//        -> chosen("'Kavga başlar'")
Kardeş sen neye baktın?
    + [Hiç, bakıyorum öyle]
     -> chosen("FirstChoice")
    + [(Ters çık)]
     ->chosen("SecondChoice")
=== chosen(TestChoice) ===
//You Chose {TestChoice}
{TestChoice: Neye bakıyon lan neye bakıyon kurabiye var simit var neye bakıyon | Lan bas git}
-> END