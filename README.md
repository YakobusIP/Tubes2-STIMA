# Tubes2-STIMA
Disusun untuk Tugas Besar 2 IF2211 Strategi Algoritma.

## Deskripsi Singkat
Fitur pencarian pada File Explorer dapat diimplementasikan dengan teknik folder crawling, di mana mesin komputer akan mulai
mencari file yang sesuai dengan query mulai dari starting directory hingga seluruh children dari
starting directory tersebut sampai satu file pertama/seluruh file ditemukan atau tidak ada file yang ditemukan. Program ini merupakan sebuah aplikasi GUI sederhana yang memodelkan fitur dari file explorer dengan memanfaatkan algoritma Breadth First Search (BFS) dan Depth First Search (DFS).

## Requirement
- Visual Studio
- .NET framework

## Langkah-Langkah Meng-Compile Program
1. Buka Visual Studio dan pilih file Tubes Stima 2.sln
2. Lakukan build dengan menekan tombol build di bagian atas Visual Studio
3. Program akan terbuka dan anda bisa mencari nama file yang dibutuhkan

## Cara Menggunakan Program
1. Ketika program terbuka, masukkan nama root folder di kiri bawah
2. Masukkan nama file yang ingin dicari, diakhiri dengan extensinya, misalkan *File1.txt*
3. Pilih algoritma yang ingin digunakan, antara BFS (Breadth First Search) atau DFS (Depth First Search)
4. Apabila ingin mencari seluruh kemunculan file, pilih checkbox All Occurrence (Opsional)
5. Tekan tombol search dan program akan mulai melakukan pencarian
6. Ketika program selesai mencari, di kanan atas drop-down list akan terisi dengan hyperlink menuju lokasi file

## Author
Monica Adelia - 13520096
Ilham Bintang - 13520102
Yakobus Iryanto Prasethio - 13520104