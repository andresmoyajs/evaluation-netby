import { EyeIcon } from "lucide-react";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import type { Product } from "@/types/product";

export default function ProductDetailModal({
  product,
  setOpen,
  open,
}: {
  product: Product;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  open: boolean;
}) {
  return (
    <Dialog
      open={open}
      onOpenChange={(open) => {
        setOpen(open);
      }}
    >
      <DialogContent className="max-w-lg">
        <DialogHeader>
          <DialogTitle>{product.name}</DialogTitle>
          <DialogDescription>
            Información detallada del producto
          </DialogDescription>
        </DialogHeader>

        <div className="space-y-4">
          {product.images[0] && (
            <img
              src={product.images[0].url}
              alt={product.name}
              className="w-48 h-48 object-cover rounded mx-auto"
            />
          )}

          <p className="text-sm text-gray-600">{product.description}</p>

          <div className="grid grid-cols-2 gap-4 text-sm">
            <p>
              <strong>Precio:</strong> ${product.price}
            </p>
            <p>
              <strong>Stock:</strong> {product.stock}
            </p>
            <p className="col-span-2 flex items-center">
              <strong>Estado:</strong>
              <span
                className={`inline-block h-3 w-3 rounded-full ml-2 mr-1 ${
                  product.statusLabel === "ACTIVO"
                    ? "bg-green-500"
                    : "bg-red-500"
                }`}
              />
              {product.statusLabel}
            </p>
          </div>
        </div>
      </DialogContent>
    </Dialog>
  );
}
