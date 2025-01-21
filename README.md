# README - AR Meubelplaatsing Project

## **Projectnaam:**

AR Meubelplaatsing

## **Beschrijving:**

Dit project is een Augmented Reality (AR) toepassing waarin gebruikers virtueel meubels kunnen plaatsen, verplaatsen, schalen en roteren in hun omgeving via een mobiele AR-app. De app biedt de mogelijkheid om drie meubelstukken (een sofa, een lamp en een tafel) te selecteren via een UI-menu en deze in een fysieke ruimte te visualiseren. Het project is bedoeld als een prototype voor interactieve en gebruiksvriendelijke AR-toepassingen in interieurontwerp.

---

## **Installatie en configuratie:**

1. **Vereisten:**
    
    - Unity Editor (versie 2021.3 of hoger met ondersteuning voor AR Foundation en ARCore/ARKit).
        
    - Een Android- of iOS-apparaat met ondersteuning voor ARCore of ARKit.
        
    - Geïnstalleerde Unity-modules voor mobiele platforms (Android/iOS Build Support).
        
2. **Project instellen:**
    
    - Download de projectbestanden en open het project in Unity.
        
    - Controleer of de volgende Unity-packages zijn geïnstalleerd:
        
        - AR Foundation
            
        - ARCore XR Plugin (voor Android) of ARKit XR Plugin (voor iOS).
            
    - Stel het "Build Settings"-platform in op Android of iOS.
        
3. **Prefab-configuratie:**
    
    - De prefab-modellen voor de sofa, lamp en tafel bevinden zich in de map `Assets/Prefabs`.
        
    - Controleer of de schaal van de modellen correct is ingesteld (bijv. `0.05` of `0.1`).
        
4. **UI-configuratie:**
    
    - Het UI-menu is onderdeel van de Canvas in de hiërarchie. Pas de tekst, kleuren of knoppen aan in `Canvas/MenuContainer` indien nodig.
        

---

## **Gebruiksaanwijzing:**

1. **Opstarten:**
    
    - Start de app op een AR-geschikt apparaat.
        
    - Beweeg het apparaat rond om de vloer te scannen. Een animatie en tekst zullen aangeven wanneer het scannen actief is.
        
2. **Meubels selecteren en plaatsen:**
    
    - Gebruik het menu onderaan het scherm om een meubel te selecteren (Sofa, Lamp of Tafel).
        
    - Tik op een oppervlak om het geselecteerde meubelstuk te plaatsen.
        
3. **Meubels manipuleren:**
    
    - Verplaats het geplaatste meubel door te tikken en een nieuwe positie te selecteren.
        
    - Gebruik twee vingers om het object te roteren of te schalen.
        
4. **Beperkingen:**
    
    - Elk meubel kan slechts één keer worden geplaatst.
        
    - Nieuwe meubels kunnen worden toegevoegd zonder dat eerder geplaatste meubels verdwijnen.
        

---

## **Features:**

1. **Objectplaatsing:**
    
    - Tik op een gedetecteerd AR-vlak om meubels te plaatsen.
        
2. **Objectmanipulatie:**
    
    - Verplaats, schaal en roteer geplaatste objecten met touch-gebaren.
        
3. **UI-selectie:**
    
    - Intuïtief menu voor het selecteren van meubels.
        
4. **Scanning-indicator:**
    
    - Visuele animatie en tekst om aan te geven dat het apparaat een oppervlak scant.
        

---

## **Structuur van het project:**

- `Assets/Prefabs` - Bevat de prefab-modellen voor meubels.
    
- `Assets/Scripts` - Bevat alle scripts, waaronder:
    
    - `ObjectManipulator.cs` - Regelt het plaatsen en manipuleren van objecten.
        
    - `PlaneDetectionUI.cs` - Regelt de "Scanning UI".
        
- `Assets/Scenes` - Bevat de hoofdscène (`SampleScene`).
    
- `Assets/Textures` - Bevat eventuele gebruikte materialen en texturen.
    

---

## **Bekende problemen en oplossingen:**

1. **Objecten zijn te groot of te klein bij plaatsing:**
    
    - Controleer de schaal van de prefab-modellen in de prefab-map.
        
2. **UI wordt niet weergegeven:**
    
    - Controleer of de Canvas-rendering is ingesteld op "Screen Space - Overlay".
        
3. **AR-plane detectie werkt niet:**
    
    - Controleer of de ARCore XR Plugin correct is ingesteld en dat de AR Session actief is.
        

---
