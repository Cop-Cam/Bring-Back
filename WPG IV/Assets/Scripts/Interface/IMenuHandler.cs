///<summary>
/// How To Use :
/// 1. Implement The Required Method to be implemented that called Processing method
/// 2. Create A Method to call UIManager method (OpenMenu and CloseMenu) that will call this interface method 
/// 3. Implement a Processing Method for doing the wanted process
///</summary>
public interface IMenuHandler
{
    void OpeningMenu();
    void ClosingMenu();
}