/*
Să se citească toate subcheile unei chei (la alegere) din Registry
și să se afișeze la ieșirea standard.
*/

#include <windows.h>
#include <stdio.h>
#include <tchar.h>

#define MAX_BUFFER 255

void ListareRegistre(HKEY hKeyRoot, LPCTSTR caleaCheii)
{
    HKEY hCheieSubiacenta;
    DWORD contor = 0;
    DWORD lungime;
    TCHAR denumireSubcheie[MAX_BUFFER];

    LSTATUS status = RegOpenKeyEx(hKeyRoot, caleaCheii, 0, KEY_READ, &hCheieSubiacenta);

    if (status != ERROR_SUCCESS)
    {
        if (status == ERROR_FILE_NOT_FOUND)
        {
            printf("Eroare: Calea specificata nu exista.\n");
        }
        else
        {
            printf("Eroare la accesarea registrului.\n");
        }
        return;
    }

    printf("Se enumera subcheile din: %s\n", caleaCheii);

    while (TRUE)
    {
        lungime = MAX_BUFFER;
        status = RegEnumKeyEx(hCheieSubiacenta, contor, denumireSubcheie, &lungime, NULL, NULL, NULL, NULL);

        if (status == ERROR_SUCCESS)
        {
            printf("Gasit: %s (Index: %ld)\n", denumireSubcheie, contor);
            contor++;
        }
        else if (status == ERROR_NO_MORE_ITEMS)
        {
            printf("\nFinalizat. Total intrari identificate: %ld\n", contor);
            break;
        }
        else
        {
            break;
        }
    }

    RegCloseKey(hCheieSubiacenta);
}

int main(void)
{
    ListareRegistre(HKEY_CURRENT_USER, TEXT("SOFTWARE\\Microsoft"));

    return 0;
}