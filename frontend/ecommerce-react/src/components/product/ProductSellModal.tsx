import { useState } from "react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { toast } from "sonner";
import { Loader2 } from "lucide-react";
import type { Product } from "@/types/product";
import { sellProduct } from "@/actions/productsAction";

export default function ProductSellModal({
  product,
  setOpen,
  open,
}: {
  product: Product;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  open: boolean;
}) {
  const queryClient = useQueryClient();
  const [quantity, setQuantity] = useState<number>(1);

  const mutation = useMutation({
    mutationFn: () => sellProduct(product.id, quantity),
    onSuccess: () => {
      toast.success(`Has vendido ${quantity} unidad(es) de ${product.name}`);
      queryClient.invalidateQueries({ queryKey: ["products"] });
      setOpen(false);
    },
    onError: (error: any) => {
      toast.error(error?.message || "Error al vender el producto");
    },
  });

  const handleSell = () => {
    if (quantity <= 0) {
      toast.error("La cantidad debe ser mayor a 0");
      return;
    }
    if (quantity > product.stock) {
      toast.error(
        `No puedes vender más del stock disponible (${product.stock})`
      );
      return;
    }
    mutation.mutate();
  };

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogContent className="max-w-md rounded-2xl p-6">
        <DialogHeader>
          <DialogTitle className="text-lg font-bold text-center">
            Vender Producto
          </DialogTitle>
        </DialogHeader>

        <div className="space-y-6">
          <div className="flex flex-col space-y-1">
            <Label className="text-sm text-muted-foreground">Producto</Label>
            <p className="font-semibold text-base">{product.name}</p>
          </div>

          <div className="flex flex-col space-y-1">
            <Label className="text-sm text-muted-foreground">
              Stock disponible
            </Label>
            <p className="text-base">{product.stock}</p>
          </div>

          <div className="flex flex-col space-y-2">
            <Label className="text-sm text-muted-foreground">
              Cantidad a vender
            </Label>
            <Input
              type="number"
              min={1}
              max={product.stock}
              value={quantity}
              onChange={(e) => setQuantity(Number(e.target.value))}
              className="rounded-lg"
            />
          </div>
        </div>

        <DialogFooter className="flex justify-end gap-3 mt-6">
          <Button
            type="button"
            variant="secondary"
            onClick={() => setOpen(false)}
            disabled={mutation.isPending}
            className="rounded-xl px-4"
          >
            Cancelar
          </Button>
          <Button
            type="button"
            onClick={handleSell}
            disabled={mutation.isPending}
            className="rounded-xl px-4"
          >
            {mutation.isPending && (
              <Loader2 className="h-4 w-4 animate-spin mr-2" />
            )}
            Vender
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
