<h4>compilation run notes</h4>
<b>
<p>
open folder in terminal (cmd + enter in File Explorer path field)
git clone git@github.com:Mateuszq28/CrudContactListMvc.git
cd CrudContactListMvc
dotnet build
cp -r wwwroot ./bin/Debug/net8.0/wwwroot
./bin/Debug/net8.0/CrudContactListMvc
ctr+shift+t
explorer "http://localhost:5000/"
</p>
<br>
<h4>Alternative command</h4>
dotnet run build