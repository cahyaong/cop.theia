from .chemical_compound import ChemicalCompound
from .cop_error import CopError
from rdkit import Chem
from rdkit.Chem import Draw
from rdkit.Chem.rdchem import Mol


class ChemicalBuilder:

    def __init__(self):
        pass

    @staticmethod
    def build_compound_descriptors(compound):
        assert compound is not None
        assert isinstance(compound, ChemicalCompound)
        assert compound.canonical_smiles is not None

        molecule = Chem.MolFromSmiles(compound.canonical_smiles)

        if molecule is None:
            raise CopError('Invalid chemical compound! Smiles: [{0}]'.format(compound.canonical_smiles))

        assert isinstance(molecule, Mol)

        compound.flat_depiction = Draw.MolToImage(molecule, (80, 80))

        compound.atom_count = molecule.GetNumAtoms(onlyExplicit=False)
        compound.explicit_atom_count = molecule.GetNumAtoms(onlyExplicit=True)

        compound.bond_count = molecule.GetNumBonds(onlyHeavy=False)
        compound.heavy_bond_count = molecule.GetNumBonds(onlyHeavy=True)
