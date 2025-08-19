import { Link } from "react-router-dom";
import { Avatar, AvatarFallback, AvatarImage } from "../ui/avatar";
import {
    NavigationMenu,
    NavigationMenuItem,
    NavigationMenuLink,
    NavigationMenuList,
} from "../ui/navigation-menu";

export default function Footer() {
  return (
    <footer className="border-t bg-background px-6 py-6 mt-auto">
      <div className="max-w-7xl mx-auto flex flex-col md:flex-row items-center justify-between gap-4">
        
        <div className="flex items-center gap-2">
          <Avatar>
            <AvatarImage src="https://github.com/shadcn.png" alt="Netby" />
            <AvatarFallback>NB</AvatarFallback>
          </Avatar>
          <span className="font-bold text-lg">Netby</span>
        </div>

        <NavigationMenu>
          <NavigationMenuList className="flex gap-4">
            <NavigationMenuItem>
              <NavigationMenuLink asChild>
                <Link to="/" className="px-2 py-1 hover:underline">
                  Inicio
                </Link>
              </NavigationMenuLink>
            </NavigationMenuItem>
            <NavigationMenuItem>
              <NavigationMenuLink asChild>
                <Link to="/productos" className="px-2 py-1 hover:underline">
                  Productos
                </Link>
              </NavigationMenuLink>
            </NavigationMenuItem>
           
        
          </NavigationMenuList>
        </NavigationMenu>

      </div>

      <div className="mt-4 text-center text-sm text-muted-foreground">
        © {new Date().getFullYear()} Netby. Todos los derechos reservados.
      </div>
    </footer>
  );
}
