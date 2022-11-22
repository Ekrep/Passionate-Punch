    Kardeş, sen neye baktın?
        + [Hiç bakıyorum öyle]
            -> isFirstTrue
        + [(Ters çıkış)]
            -> isFirstFalse

=== isFirstTrue ===
Neye bakıyon lan kurabiye var simit var neye bakıyon
    + [Manyak mısın sen be?]
        -> isSecondTrue
    + [Özür dilerim hemen gidiyorum.]
        -> isFirstFalse
-> END
=== isFirstFalse === 
Çık dışarı, çık!
-> END

=== isSecondTrue ===
Lan manyaaaaaaağm
-> END